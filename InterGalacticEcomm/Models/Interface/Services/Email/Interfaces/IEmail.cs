using InterGalacticEcomm.Models.Interface.Services.Email.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface.Services.Email.Interfaces
{
    public interface IEmail
    {
        public Task<EmailResponse> SendEmailAsync(Message inboundData);
    }
}
