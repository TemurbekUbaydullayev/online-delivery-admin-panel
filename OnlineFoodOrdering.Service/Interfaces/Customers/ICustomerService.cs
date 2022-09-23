using OnlineFoodOrdering.Domain.Common;
using OnlineFoodOrdering.Domain.Entities;
using OnlineFoodOrdering.Service.DTOs.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.Interfaces.Customers
{
    public interface ICustomerService
    {
        Task<Customer> CreateAsync(CustomerForCreationDto model);

        Task<Customer> UpdateAsync(int id, CustomerForCreationDto model);

        Task<bool> DeleteAsync(Expression<Func<Customer, bool>> expression);

        Task<Customer?> GetAsync(Expression<Func<Customer, bool>> expression);

        Task<IEnumerable<Customer>> GetAllAsync(Expression<Func<Customer, bool>>? expression = null,
            PaginationParameters? parameters = null);
    }
}
