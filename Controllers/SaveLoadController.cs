using CST350_CLC.Models;
using CST350_CLC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST350_CLC.Controllers
{
    public class SaveLoadController : Controller
    {
        [CustomAuthorization]
        public IActionResult Index()
        {
            return View();
        }
        [CustomAuthorization]
        public ActionResult SaveGame()
        {
            UserModel user = SecurityService.GetUserByUsername(HttpContext.Session.GetString("username"));

            if (SecurityService.CreateGameSave(user, CellBusinessService.cells) == true)
            {
                return Content("Game was saved!");
            }
            else
            {
                return Content("ERROR: Game could not be saved!");
            }
        }

        [CustomAuthorization]
        public IActionResult LoadMenu()
        {
            UserModel user = SecurityService.GetUserByUsername(HttpContext.Session.GetString("username"));
            List<GameSaveModel> saves = SecurityService.GetUsersSavedGames(user);

            return View("LoadMenu", saves);
        }

        [CustomAuthorization]
        public ActionResult Load(int id)
        {
            return View("../Cell/Index", CellBusinessService.LoadGame(id));
        }

        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            SecurityService.DeleteGameSave(id);

            UserModel user = SecurityService.GetUserByUsername(HttpContext.Session.GetString("username"));
            List<GameSaveModel> saves = SecurityService.GetUsersSavedGames(user);

            return View("LoadMenu", saves);
        }
    }
}
