using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Models;

namespace SiteMVC.Controllers {
    public class AccountController : Controller {
        private readonly SignInManager<WebsiteUser> _signInManager;
        public AccountController(SignInManager<WebsiteUser> signInManager) {
            _signInManager = signInManager;
        }
        [HttpGet]
        public ActionResult Login() {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            // Que fait cette condition ? Pris dans le skillpipe
            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
                if (result.Succeeded) {
                    // TODO : redirection après Login. À la page de départ ? voir skillpipe
                    if (Request.Query.Keys.Contains("ReturnUrl")) {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else {
                        return RedirectToAction("Index", "Tirelires");
                    }
                }
            }
            return View();
        }
    }
}
