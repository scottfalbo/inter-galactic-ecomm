using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CartId { get; set; }
        //assign the order to the cart
        public Cart Cart { get; set; }

        public bool Shipped { get; set; }
        public bool Recieved { get; set; }
        public bool Paid { get; set; }
    }
}
