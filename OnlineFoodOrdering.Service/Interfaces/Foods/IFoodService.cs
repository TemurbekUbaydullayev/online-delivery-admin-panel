using OnlineFoodOrdering.Domain.Common;
using OnlineFoodOrdering.Domain.Entities;
using OnlineFoodOrdering.Service.DTOs.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.Interfaces.Foods
{
    public interface IFoodService
    {
        Task<FoodViewModel> CreateAsync(FoodForCreationDto model);

        Task<FoodViewModel> UpdateAsync(int id, FoodForCreationDto model);

        Task<bool> DeleteAsync(Expression<Func<Food, bool>> expression);

        Task<FoodViewModel?> GetAsync(Expression<Func<Food, bool>> expression);

        Task<IEnumerable<FoodViewModel>> GetAllAsync(Expression<Func<Food, bool>>? expression = null,
            PaginationParameters? parameters = null);
    }
}
