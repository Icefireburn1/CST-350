using CST350_CLC.Interfaces;
using CST350_CLC.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CST350_CLC.Services
{
    public static class CellBusinessService
    {
        public static List<CellModel> cells = new List<CellModel>();
        const int GRID_SIZE = 100;
        static int lives = -1;
        static string gameState = "Index";

        public static IDifficulty SelectedDifficulty { get; set; }

        // Sets up Minesweeper board
        public static List<CellModel> SetupGame()
        {
            ResetBoard();
            SetupLiveNeighbors();
            CalculateLiveNeighbors();

            return cells;
        }

        public static int GetPercentFinished(string gameData)
        {
            List<string> states = gameData.Split(",").ToList();

            int finished = 0;
            foreach(string s in states)
            {
                if (s == "1")
                {
                    finished++;
                }
            }

            return finished;
        }

        // Handles clicks && flags
        public static List<CellModel> ClickHandling(int cellNumber, bool flag)
        {
            // Make cell flag
            if (flag && cells.ElementAt(cellNumber).CellState == 0)
            {
                cells.ElementAt(cellNumber).CellState = 3;
                return cells;
            }
            // Make flag a normal cell
            else if (flag && cells.ElementAt(cellNumber).CellState == 3)
            {
                cells.ElementAt(cellNumber).CellState = 0;
                return cells;
            }
            // If flag and trying to click, don't do anything
            else if (cells.ElementAt(cellNumber).CellState == 3)
            {
                return cells;
            }
            // Reveal a single cell
            else if (cells.ElementAt(cellNumber).isBomb == false && cells.ElementAt(cellNumber).Neighbors > 0)
            {
                cells.ElementAt(cellNumber).CellState = 1;
            }
            // Found a bomb
            else if (SelectedDifficulty.CheckForBomb(cells.ElementAt(cellNumber)))
            {
                cells.ElementAt(cellNumber).CellState = 2;

                // Continue if we still have lives
                if (lives > 1)
                {
                    lives -= 1;

                    return cells;
                }

                List<CellModel> temp = cells;
                ResetBoard();

                gameState = "GameOver";

                return temp;
            }
            FloodFill(cellNumber);

            if (SelectedDifficulty.CheckForWin(cells))
            {
                List<CellModel> temp = cells;
                ResetBoard();

                gameState = "Victory";
                return temp;
            }

            return cells;
        }

        // Handles Game Over and Victory
        public static string GetView()
        {
            return gameState;
        }

        // Decide which cells will become live (bombs)
        private static void SetupLiveNeighbors()
        {
            cells = SelectedDifficulty.CreateBombs(cells);
        }

        // Generates the # of neighbors for each cell
        private static void CalculateLiveNeighbors()
        {
            int count;

            int i = 0;
            foreach (var cell in cells)
            {
                // Cell is live, set to 9
                if (cell.isBomb)
                {
                    cell.Neighbors = 9;
                    i++;
                    continue;
                }

                count = 0;

                // Left Neighbors
                if (((i % 10) != 0) && (i >= 10) && (i - 11 >= 0) && cells.ElementAt(i - 11).isBomb) count++; // Deny left column, top row, then check top left
                if (((i % 10) != 0) && (i <= 89) && (i + 9 >= 0) && cells.ElementAt(i + 9).isBomb) count++; // Deny left column, bottom row, then check bottom left
                if (((i % 10) != 0) && cells.ElementAt(i - 1).isBomb) count++; // Deny left column, then check left

                // Right Neighbors
                if (((i.ToString().Contains("9") == false) && (i >= 10) && (i - 9 >= 0) && cells.ElementAt(i - 9).isBomb)) count++; // Deny Right column, top row, then check top right
                if (((i.ToString().Contains("9") == false) && (i <= 89) && (i + 11 >= 0) && cells.ElementAt(i + 11).isBomb)) count++; // Deny Right column, bottom row, then check bottom right
                if (((i.ToString().Contains("9") == false) && cells.ElementAt(i + 1).isBomb)) count++; // Deny Right column, then check right

                // Top Bottom
                if ((i >= 10) && (i - 10 >= 0) && cells.ElementAt(i - 10).isBomb) count++; // Top
                if ((i <= 89) && (i + 10 <= 99) && cells.ElementAt(i + 10).isBomb) count++; // Bottom


                cells.ElementAt(i).Neighbors = count;

                i++;
            }
        }

        // Potential bug with separated cells becoming visited
        private static void FloodFill(int id)
        {
            if (id < 0 || id >= GRID_SIZE || cells.ElementAt(id).isBomb || cells.ElementAt(id).CellState == 1 || cells.ElementAt(id).Neighbors > 0)
                return;

            cells.ElementAt(id).CellState = 1;

            if (id.ToString().Contains("9") == false)
                FloodFill(id + 1); // Check Right
            if (id % 10 != 0)
                FloodFill(id - 1); // Check Left
            if (id >= 10)
                FloodFill(id - 10); // Check Up
            if (id <= 89)
                FloodFill(id + 10); // Check Down
        }

        // Resets Minesweeper Game
        private static void ResetBoard()
        {
            gameState = "Index";
            lives = SelectedDifficulty.GetStartingLives();
            cells = new List<CellModel>();
            if (cells.Count < GRID_SIZE)
            {
                for (int i = 0; i < GRID_SIZE; i++)
                {

                    cells.Add(new CellModel { Id = i, CellState = 0, isBomb = false });
                }
            }

            SetupLiveNeighbors();
            CalculateLiveNeighbors();
        }

        // Starts Minesweeper Game
        public static List<CellModel> StartGame()
        {
            ResetBoard();

            return cells;
        }

        // Loads the game from our database
        public static List<CellModel> LoadGame(int id)
        {
            ResetBoard();

            List<string> cellStates = SecurityService.GetGameById(id);
            int state;
            for(int i = 0; i < cells.Count; i++)
            {
                state = Int32.Parse(cellStates.ElementAt(i));

                // Check if bomb
                if (state == -1)
                {
                    cells[i].CellState = 0;
                    cells[i].isBomb = true;
                    continue;
                }

                cells[i].CellState = Int32.Parse(cellStates.ElementAt(i));
            }

            CalculateLiveNeighbors();

            return cells;
        }
    }
}
