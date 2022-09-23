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
    public interface IOrderDetailService
    {
        Task<OrderDetailViewModel> CreateAsync(OrderDetailForCreationDto model);

        Task<OrderDetailViewModel> UpdateAsync(int id, OrderDetailForCreationDto model);

        Task<bool> DeleteAsync(Expression<Func<OrderDetail, bool>> expression);

        Task<OrderDetailViewModel?> GetAsync(Expression<Func<OrderDetail, bool>> expression);

        Task<IEnumerable<OrderDetailViewModel>> GetAllAsync(Expression<Func<OrderDetail, bool>>? expression = null,
            PaginationParameters? parameters = null);
    }
}
