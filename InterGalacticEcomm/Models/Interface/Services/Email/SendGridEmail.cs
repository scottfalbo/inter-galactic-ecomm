using InterGalacticEcomm.Models.Interface.Services.Email.Interfaces;
using InterGalacticEcomm.Models.Interface.Services.Email.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface.Services.Email
{
    public class SendGridEmail : IEmail
    {
        public IConfiguration Config { get; set; }

        public SendGridEmail(IConfiguration config)
        {
            Config = config;
        }


        public async Task<EmailResponse> SendEmailAsync(Message inboundData)
        {
            var apiKey = Config["SendGrid:Key"];
            var email = Config["SendGrid:FromEmail"];
            var name = Config["SendGrid:FromName"];
            var client = new SendGridClient(apiKey);

            SendGridMessage msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(email, name));
            msg.AddTo(inboundData.To);
            msg.SetSubject(inboundData.Subject);
            msg.AddContent(MimeType.Html, inboundData.Body);


            var response = await client.SendEmailAsync(msg);
            EmailResponse emailResponse = new EmailResponse()
            {
                WasSent = response.IsSuccessStatusCode
            };

            return emailResponse;
        }
    }
}