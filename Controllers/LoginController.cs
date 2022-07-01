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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            UserModel verifiedUser = SecurityService.GetUser(user);
            if (verifiedUser != null)
            {
<<<<<<< HEAD
                // Remember user
                HttpContext.Session.SetString("username", user.username);

=======
>>>>>>> 98796512f41c8100cae98063bf189a0963bbdc9f
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
