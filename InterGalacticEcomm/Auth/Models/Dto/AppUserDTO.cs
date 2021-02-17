using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.API
{
    public class AppUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public List<string> Roles { get; set; }
    }
}
