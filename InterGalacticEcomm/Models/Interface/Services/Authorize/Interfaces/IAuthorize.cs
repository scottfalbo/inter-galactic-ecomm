using InterGalacticEcomm.Models.Interface.Services.Authorize.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface.Services.Authorize.Interfaces
{
    public interface IAuthorize
    {
        public bool AuthorizeCard(CreditCard creditCard, decimal totalPrice);
    }
}
