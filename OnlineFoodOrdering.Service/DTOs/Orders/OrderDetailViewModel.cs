using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.DTOs.Orders
{
    public class OrderDetailViewModel
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string FoodName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }
    }
}
