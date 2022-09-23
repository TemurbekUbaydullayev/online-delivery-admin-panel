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
    public interface IFoodTypeService
    {
        Task<FoodType> CreateAsync(FoodTypeForCreationDto model);

        Task<FoodType> UpdateAsync(int id, FoodTypeForCreationDto model);

        Task<bool> DeleteAsync(Expression<Func<FoodType, bool>> expression);

        Task<FoodType?> GetAsync(Expression<Func<FoodType, bool>> expression);

        Task<IEnumerable<FoodType>> GetAllAsync(Expression<Func<FoodType, bool>>? expression = null,
            PaginationParameters? parameters = null);
    }
}
