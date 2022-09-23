using OnlineFoodOrdering.Domain.Common;
using OnlineFoodOrdering.Domain.Entities;
using OnlineFoodOrdering.Service.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<OrderViewModel> CreateAsync(OrderForCreationDto model);

        Task<OrderViewModel> UpdateAsync(int id, OrderForCreationDto model);

        Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression);

        Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> expression);

        Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null,
            PaginationParameters? parameters = null);
    }
}
