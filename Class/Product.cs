using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Class
{
    public class Product
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductLine { get; set; }
        public string ProductScale { get; set; }
        public string ProductVendor { get; set; }
        public string ProductDescription { get; set; }
        public int QuantityInStock { get; set; }
        public double BuyPrice { get; set; }
        public double MSRP { get; set; }

        public Product(string productCode, string productName, string productLine, string productScale, string productVendor, string productDescription, int quantityInStock, double buyPrice, double mSRP)
        {
            ProductCode = productCode;
            ProductName = productName;
            ProductLine = productLine;
            ProductScale = productScale;
            ProductVendor = productVendor;
            ProductDescription = productDescription;
            QuantityInStock = quantityInStock;
            BuyPrice = buyPrice;
            MSRP = mSRP;
        }
    }
}
