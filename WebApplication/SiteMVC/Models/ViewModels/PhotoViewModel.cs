using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.Models.ViewModels {
    public class PhotoViewModel {
        public int? IdProduit { get; set; }

        public IFormFile PhotoFile { get; set; }

    }
}
