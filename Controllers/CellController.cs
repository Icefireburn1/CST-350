using CST350_CLC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CST350_CLC.Controllers
{
    public class CellController : Controller
    {
        static List<CellModel> cells = new List<CellModel>();
        Random random = new Random();
        const int GRID_SIZE = 100;

        public IActionResult Index()
        {
            if(cells.Count < GRID_SIZE)
            { 
                for(int i = 0; i < GRID_SIZE; i++)
                {
                    cells.Add(new CellModel { Id = i, CellState = 0 });
                }
            }
            return View("Index", cells);
        }

        public IActionResult HandleCellClick(string cellNumber)
        {
            int c = int.Parse(cellNumber);

            cells.ElementAt(c).CellState = (cells.ElementAt(c).CellState + 1) % 2;
            
            return View("Index", cells);
        }
    }
}
