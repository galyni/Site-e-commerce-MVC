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
        private readonly UserManager<WebsiteUser> _userManager;
        public AccountController(SignInManager<WebsiteUser> signInManager, UserManager<WebsiteUser> userManager) {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                WebsiteUser user = new WebsiteUser {
                    UserName = model.Username,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    return await Login(model);
                }
            }
            return View();
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
                    // idem pour Logout
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
        // TODO page de confirmation Logout ?
        public ActionResult Logout() {
            if (User.Identity.IsAuthenticated) {
                _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Tirelires");
        }
    }
}
