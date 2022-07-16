using CST350_CLC.Interfaces;
using CST350_CLC.Models;
using CST350_CLC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CST350_CLC.Controllers
{
    public class CellController : Controller
    {

        [CustomAuthorization]
        public IActionResult Index()
        {
            ViewBag.DifficultyName = CellBusinessService.SelectedDifficulty.GetDifficultyName(); // Send difficulty name to view
            return View("Index", CellBusinessService.SetupGame());
        }

        // handles cell clicks
        [CustomAuthorization]
        public IActionResult HandleCellClick(int cellNumber)
        {
            ViewBag.DifficultyName = CellBusinessService.SelectedDifficulty.GetDifficultyName(); // Send difficulty name to view
            return View(CellBusinessService.GetView(), CellBusinessService.ClickHandling(cellNumber, false));
        }

        // handles flagging and bombs for cells
        [CustomAuthorization]
        public IActionResult ShowOneCell(int cellNumber, bool flag)
        {
            ViewBag.DifficultyName = CellBusinessService.SelectedDifficulty.GetDifficultyName(); // Send difficulty name to view

            List<CellModel> cells = CellBusinessService.ClickHandling(cellNumber, flag);          // Make object here so we call it before GetView()
            return PartialView(CellBusinessService.GetView(), cells);
        }
    }
}