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
<<<<<<< HEAD

        [CustomAuthorization]
        public IActionResult Index()
        {
            return View("Index", CellBusinessService.SetupGame());
        }

        // handles cell clicks
        [CustomAuthorization]
        public IActionResult HandleCellClick(int cellNumber)
        {
            return View(CellBusinessService.GetView(cellNumber), CellBusinessService.ClickHandling(cellNumber));
        }

        // handles flagging and bombs for cells
        [CustomAuthorization]
        public IActionResult ShowOneCell(int cellNumber, bool flag)
        {
            List<CellModel> repo = CellBusinessService.FlagHandling(cellNumber, flag); // Make object here so we call it before GetView()
            return PartialView(CellBusinessService.GetView(cellNumber), repo);
=======
        CellBusinessService repository = new CellBusinessService();

        public IActionResult Index()
        {
            return View("Index", repository.SetupGame());
        }

        // handles cell clicks
        public IActionResult HandleCellClick(int cellNumber)
        {
            return View(repository.GetView(cellNumber), repository.ClickHandling(cellNumber));
        }

        // handles flagging and bombs for cells
        public IActionResult ShowOneCell(int cellNumber, bool flag)
        {
            List<CellModel> repo = repository.FlagHandling(cellNumber, flag); // Make object here so we call it before GetView()
            return PartialView(repository.GetView(cellNumber), repo);
>>>>>>> 98796512f41c8100cae98063bf189a0963bbdc9f
        }
    }
}