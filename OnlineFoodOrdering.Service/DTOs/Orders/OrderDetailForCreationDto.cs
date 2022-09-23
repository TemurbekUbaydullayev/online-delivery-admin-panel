using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.DTOs.Orders
{
    public class OrderDetailForCreationDto
    {
        public long OrderId { get; set; }

        public long FoodId { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }
    }
}
