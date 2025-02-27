using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Class
{
    internal class Customer
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }

        public Customer(string name, string phone, string city)
        {
            Name = name;
            Phone = phone;
            City = city;
        }
    }
}
