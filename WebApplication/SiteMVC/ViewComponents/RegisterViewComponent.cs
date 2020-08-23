using Microsoft.AspNetCore.Mvc;
using SiteMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.ViewComponents {
    public class RegisterViewComponent : ViewComponent {
        public Task<IViewComponentResult> InvokeAsync() {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return Task.FromResult<IViewComponentResult>(View("Register", registerViewModel));
        }
    }
}
