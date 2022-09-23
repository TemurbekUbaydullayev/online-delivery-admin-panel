using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.DTOs.Foods
{
    public class FoodForCreationDto
    {
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public long? FoodTypeId { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
    }
}
