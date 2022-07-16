using CST350_CLC.Interfaces;
using CST350_CLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST350_CLC.Utility
{
    public class EasyDifficulty : IDifficulty
    {
        readonly int bombSpawnThreshold = 1; // 2% chance to spawn a bomb since 0 is inclusive

        public bool CheckForBomb(CellModel cell)
        {
            // Say no if player already clicked bomb
            if (cell.isBomb == true && cell.CellState != 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    public bool CheckForWin(List<CellModel> cells)
        {
            bool victory = true;
            foreach (var cell in cells)
            {
                if (cell.isBomb == false && cell.CellState == 0)
                {
                    victory = false;
                }
            }

            return victory;
        }

        public List<CellModel> CreateBombs(List<CellModel> cells)
        {
            Random ran = new Random();
            foreach (var cell in cells)
            {
                int chance = ran.Next(0, 100);

                if (chance <= bombSpawnThreshold)
                    cell.isBomb = true;
            }

            return cells;
        }

        public string GetDifficultyName()
        {
            return "Easy";
        }

        public int GetStartingLives()
        {
            return 2; // Player loses when they find 2 bombs
        }
    }
}
