using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.DTOs.Orders
{
    public class OrderForCreationDto
    {
        public long CustomerId { get; set; }
        public float TotalAmount { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
    }
}
