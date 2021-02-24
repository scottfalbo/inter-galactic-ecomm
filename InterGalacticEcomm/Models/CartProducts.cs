using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models
{
    public class CartProducts
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }

    }
}
