using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IAuthProvider AuthProvider { get; set; }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!AuthProvider.Authenticate(loginViewModel.Login, loginViewModel.Password))
            {
                ModelState.AddModelError("", "Incorrect credentials");
                return View();
            }

            return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
        }
    }
}