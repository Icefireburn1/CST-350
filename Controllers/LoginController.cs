using CST350_CLC.Interfaces;
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
    public class LoginController : Controller
    {
        public LoginController(IDifficulty difficulty)
        {
            CellBusinessService.SelectedDifficulty = difficulty;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            UserModel verifiedUser = SecurityService.GetUser(user);
            if (verifiedUser != null)
            {
                // Remember user
                HttpContext.Session.SetString("username", user.username);

                // Pass difficulty name to view
                ViewBag.DifficultyName = CellBusinessService.SelectedDifficulty.GetDifficultyName(); 

                return View("../Cell/Index", CellBusinessService.StartGame());
            }
            else
            {
                HttpContext.Session.Remove("username");

                return View("LoginFailure", user);
            }
        }
    }
}
