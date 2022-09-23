using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.DTOs.Foods
{
    public class FoodViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public string FoodTypeName { get; set; } = String.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
