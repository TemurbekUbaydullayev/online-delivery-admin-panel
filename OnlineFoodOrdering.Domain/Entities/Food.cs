using OnlineFoodOrdering.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Domain.Entities
{
    public class Food : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }

        public long FoodTypeId { get; set; }

        [ForeignKey(nameof(FoodTypeId))]
        public FoodType? FoodType { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public Food()
        {

        }
    }
}
