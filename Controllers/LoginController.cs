using CST350_CLC.Models;
using CST350_CLC.Services;
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
            SecurityService securityService = new SecurityService();
            UserModel verifiedUser = securityService.GetUser(user);
            if (verifiedUser != null)
            {
                return View("../Cell/Index", CellController.StartGame());
            }
            else
            {
                return View("LoginFailure", user);
            }
        }
    }
}
