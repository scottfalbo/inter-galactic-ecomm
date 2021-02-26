using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface.Services.Authorize.Models
{
    public class CreditCard
    {
        public string CreditCardNum { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
    }
}
