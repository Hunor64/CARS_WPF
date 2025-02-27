using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Class
{
    internal class OrderDetails
    {
        public OrderDetails(string productName, double buyPrice)
        {
            this.productName = productName;
            this.buyPrice = buyPrice;
        }

        public string productName { get; set; }
        public double buyPrice { get; set; }
    }
}
