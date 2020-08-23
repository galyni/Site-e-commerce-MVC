using Microsoft.AspNetCore.Mvc;
using SiteMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.ViewComponents {
    public class LoginViewComponent : ViewComponent {
        public Task<IViewComponentResult> InvokeAsync() {
            LoginViewModel loginViewModel = new LoginViewModel();
            return Task.FromResult<IViewComponentResult>(View("Login", loginViewModel));
        }
    }
}
