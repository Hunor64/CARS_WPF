using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Class
{
    public class Order
    {
        public Order(int id, DateTime? orderDate, DateTime requiredDate, DateTime? shippedDate, string status, string comments, int customerName)
        {
            Id = id;
            CustomerNumber = customerName;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            Status = status;
            Comments = comments;
        }

        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? Status { get; set; }
        public string? Comments { get; set; }
        public int? CustomerNumber { get; set; }


    }
}
