using AutoMapper;
using OnlineFoodOrdering.Data.DbContexts;
using OnlineFoodOrdering.Data.Interfaces;
using OnlineFoodOrdering.Data.Repositories;
using OnlineFoodOrdering.Domain.Common;
using OnlineFoodOrdering.Domain.Entities;
using OnlineFoodOrdering.Service.DTOs.Foods;
using OnlineFoodOrdering.Service.Extensions;
using OnlineFoodOrdering.Service.Interfaces.Foods;
using OnlineFoodOrdering.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.Services.Foods
{
    public class FoodTypeService : IFoodTypeService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<FoodType> _foodTypeRepository;
        private readonly AppDbContext _appDbContext;

        public FoodTypeService()
        {
            _appDbContext = new AppDbContext();
            _foodTypeRepository = new GenericRepository<FoodType>(_appDbContext);
            _mapper = new Mapper(new MapperConfiguration(p => p.AddProfile<MapperProfile>()));
        }
        public async Task<FoodType> CreateAsync(FoodTypeForCreationDto dto)
        {
            var anyFoodType = await _foodTypeRepository.AnyAsync(type
                => type.Name.Equals(dto.Name));

            if (anyFoodType)
            {
                throw new Exception("This category already exist");
            }

            var foodType = _mapper.Map<FoodType>(dto);
            var resFoodType = (await _foodTypeRepository.CreateAsync(foodType)).Entity;
            await _appDbContext.SaveChangesAsync();

            return resFoodType;
        }

        public async Task<bool> DeleteAsync(Expression<Func<FoodType, bool>> expression)
        {
            var foodType = _foodTypeRepository.Where(expression);

            if (!foodType.Any())
            {
                throw new Exception("Category not found!");
            }

            _foodTypeRepository.DeleteRange(foodType);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public Task<IEnumerable<FoodType>> GetAllAsync(Expression<Func<FoodType, bool>>? expression = null,
            PaginationParameters? parameters = null)
            => Task.FromResult<IEnumerable<FoodType>>(_foodTypeRepository.Where(expression).ToPagedList(parameters));

        public Task<FoodType?> GetAsync(Expression<Func<FoodType, bool>> expression)
            => _foodTypeRepository.GetAsync(expression);

        public async Task<FoodType> UpdateAsync(int id, FoodTypeForCreationDto model)
        {
            var existFoodType = await _foodTypeRepository.GetAsync(type => type.Id == id);

            if (existFoodType is null)
            {
                throw new Exception("Category not found!");
            }
            var mappedFood = _mapper.Map(model, existFoodType);
            _foodTypeRepository.Update(mappedFood);
            await _appDbContext.SaveChangesAsync();

            return mappedFood;
        }
    }
}
