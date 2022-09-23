using OnlineFoodOrdering.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodOrdering.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public long OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        public long FoodId { get; set; }
        [ForeignKey(nameof(FoodId))]
        public Food Food { get; set; }

        public int Quantity { get; set; } = 1;


        public double TotalPrice { get; set; }

        public OrderDetail()
        {

        }
    }
}
