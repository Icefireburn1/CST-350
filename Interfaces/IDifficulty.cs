using CST350_CLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST350_CLC.Interfaces
{
    public interface IDifficulty
    {
        public string GetDifficultyName();
        public int GetStartingLives();
        public bool CheckForWin(List<CellModel> cells);
        public bool CheckForBomb(CellModel cell);
        public List<CellModel> CreateBombs(List<CellModel> cells);
    }
}
