using InterGalacticEcomm.Models.API;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        [BindProperty]
        public List<CartProducts> CartProducts { get; set; }
    }
}
