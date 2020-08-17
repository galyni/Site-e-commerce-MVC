using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.Models {
    public class WebsiteUser : IdentityUser {
        private int? Idclient;
        public WebsiteUser() {
            Idclient = null;
        }
    }
}
