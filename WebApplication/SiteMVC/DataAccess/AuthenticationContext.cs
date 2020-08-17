using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;

namespace SiteMVC.DataAccess {
    public class AuthenticationContext : IdentityDbContext<WebsiteUser> {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) {

        }
    }
}
