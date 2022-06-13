using CST350_CLC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CST350_CLC.Controllers
{
    public class CellController : Controller
    {
        public static List<CellModel> cells = new List<CellModel>();
        Random random = new Random();
        const int GRID_SIZE = 100;
        const int difficulty = 8;

        public IActionResult Index()
        {
            ResetBoard();
            SetupLiveNeighbors(difficulty);
            CalculateLiveNeighbors();
            return View("Index", cells);
        }

        public IActionResult HandleCellClick(int cellNumber)
        {

            //cells.ElementAt(cellNumber).CellState = (cells.ElementAt(cellNumber).CellState + 2);
            if (cells.ElementAt(cellNumber).isBomb == false && cells.ElementAt(cellNumber).Neighbors > 0)
            {
                cells.ElementAt(cellNumber).CellState = 1;
            }
            else if (cells.ElementAt(cellNumber).isBomb == true)
            {
                cells.ElementAt(cellNumber).CellState = 2;
                List<CellModel> temp = cells;
                ResetBoard();
                return View("GameOver", temp);
            }
            FloodFill(cellNumber);

            if (CheckForWin())
            {
                List<CellModel> temp = cells;
                ResetBoard();

                return View("Victory", temp);
            }
            

            return View("Index", cells);
        }

        // Decide which cells will become live (bombs)
        private static void SetupLiveNeighbors(int difficulty)
        {
            Random ran = new Random();
            foreach(var cell in cells)
            {
                int chance = ran.Next(0, 100);

                if (chance <= difficulty)
                    cell.isBomb = true;
            }
        }

        // Generates the # of neighbors for each cell
        private static void CalculateLiveNeighbors()
        {
            int count;

            int i = 0;
            foreach(var cell in cells)
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

        private bool CheckForWin()
        {
            bool victory = true;
            foreach(var cell in cells)
            {
                if (cell.isBomb == false && cell.CellState == 0)
                {
                    victory = false;
                }
            }

            return victory;
        }

        private static void ResetBoard()
        {
            cells = new List<CellModel>();
            if (cells.Count < GRID_SIZE)
            {

                for (int i = 0; i < GRID_SIZE; i++)
                {

                    cells.Add(new CellModel { Id = i, CellState = 0, isBomb = false });
                }
            }

            SetupLiveNeighbors(difficulty);
            CalculateLiveNeighbors();
        }

        public static List<CellModel> StartGame()
        {
            ResetBoard();

            return cells;
        }
    }
}