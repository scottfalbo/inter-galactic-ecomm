using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using InterGalacticEcomm.Models.Interface.Services.Authorize.Interfaces;
using InterGalacticEcomm.Models.Interface.Services.Authorize.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface.Services.Authorize
{
    public class AuthorizeService : IAuthorize
    {
        public bool AuthorizeCard(CreditCard creditCard, decimal totalPrice)
        {
			bool result = Run("8kdY2X7Z", "65xdb76Z5qB93LYE", creditCard, totalPrice);
            return result;
        }
		public static bool Run(string ApiLoginID, string ApiTransactionKey, CreditCard userCard, decimal totalPrice)
		{

			ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

			// define the merchant information (authentication / transaction id)
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
			{
				name = ApiLoginID,
				ItemElementName = ItemChoiceType.transactionKey,
				Item = ApiTransactionKey,
			};

		
			var creditCard = new creditCardType
			{
				//cardNumber = "4111111111111111",
				//expirationDate = "0718"
				cardNumber = userCard.CreditCardNum,
				expirationDate = userCard.Expiration,
				//cardCode = userCard.CVV
			};

			//standard api call to retrieve response
			var paymentType = new paymentType { Item = creditCard };

			var transactionRequest = new transactionRequestType
			{
				transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
				amount = totalPrice,
				payment = paymentType
			};

			var request = new createTransactionRequest { transactionRequest = transactionRequest };

			// instantiate the contoller that will call the service
			var controller = new createTransactionController(request);
			controller.Execute();

			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();

			if (response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response.transactionResponse != null)
				{
					Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
				}
			}
			else
			{
				Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
				if (response.transactionResponse != null)
				{
					Console.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
				}
			}
			return true;
		}
	}
}
