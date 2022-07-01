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
        }
    }
}