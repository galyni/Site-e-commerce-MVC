﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using SiteMVC.Models;

namespace SiteMVC.Controllers {
    public class AccountController : Controller {
        private readonly SignInManager<WebsiteUser> _signInManager;
        private readonly UserManager<WebsiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<WebsiteUser> signInManager, UserManager<WebsiteUser> userManager, RoleManager<IdentityRole> roleManager) {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public ActionResult Register() {
            ViewBag.Roles = new List<string>() {"User", "Administrator", "Moderator" }.Select(
                r => new SelectListItem { Text = r, Value = r});
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel) {
            if (ModelState.IsValid) {
                WebsiteUser user = new WebsiteUser {
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded) {
                    bool roleExists = await _roleManager.RoleExistsAsync(registerModel.RoleName);
                    if (!roleExists) {
                        await _roleManager.CreateAsync(new IdentityRole(registerModel.RoleName));
                    }
                    var addedUser = await _userManager.FindByNameAsync(registerModel.Username);
                    await _userManager.AddToRoleAsync(addedUser, registerModel.RoleName);

                    return await Login(registerModel);
                }
            }
            ViewBag.Roles = new List<string>() { "User", "Administrator", "Moderator" }.Select(
              r => new SelectListItem { Text = r, Value = r });
            return View();
        }

        [HttpGet]
        public ActionResult Login() {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel) {
            // Que fait cette condition ? Pris dans le skillpipe
            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, true, false);
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
