using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class FoodService : IFoodService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Food> _foodRepository;
        private readonly IGenericRepository<FoodType> _foodTypeRepository;
        private readonly AppDbContext _appDbContext;

        public FoodService()
        {
            _appDbContext = new AppDbContext();
            _foodRepository = new GenericRepository<Food>(_appDbContext);
            _foodTypeRepository = new GenericRepository<FoodType>(_appDbContext);
            _mapper = new Mapper(new MapperConfiguration(p => p.AddProfile<MapperProfile>()));
        }
        public async Task<FoodViewModel> CreateAsync(FoodForCreationDto model)
        {
            var anyFood = await _foodRepository.AnyAsync(food => food.Name.Equals(model.Name));

            if (anyFood)
            {
                throw new Exception("This food already exist!");
            }

            var anyFoodType = await _foodTypeRepository.GetAsync(p => p.Id == model.FoodTypeId);

            if (model.FoodTypeId != 0 || model.FoodTypeId != null)
            {
                if (anyFoodType is null)
                    throw new Exception("Food category not found!");
            }

            var food = _mapper.Map<Food>(model);
            food.CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
            food.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

            var resFood = (await _foodRepository.CreateAsync(food)).Entity;
            await _appDbContext.SaveChangesAsync();

            var viewFood = _mapper.Map<FoodViewModel>(food);

            viewFood.FoodTypeName = anyFoodType!.Name;


            return viewFood;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Food, bool>> expression)
        {
            var food = _foodRepository.Where(expression);

            if (!food.Any())
            {
                throw new Exception("Food not found!");
            }

            _foodRepository.DeleteRange(food);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<FoodViewModel>> GetAllAsync(Expression<Func<Food, bool>>? expression = null, PaginationParameters? parameters = null)
        {
            var foods = _foodRepository.Where(expression).Include(food => food.FoodType).ToPagedList(parameters);

            var viewMaps = new List<FoodViewModel>();

            foreach (var food in foods)
            {
                var f = _mapper.Map<FoodViewModel>(food);
                f.FoodTypeName = food.FoodType!.Name;

                viewMaps.Add(f);
            }

            return viewMaps;
        }

        public async Task<FoodViewModel?> GetAsync(Expression<Func<Food, bool>> expression)
        {
            var food = await _foodRepository.GetAsync(expression);

            if (food is null)
            {
                throw new Exception("Food not found!");
            }

            var viewMap = _mapper.Map<FoodViewModel>(food);

            viewMap.FoodTypeName = (await _foodTypeRepository.GetAsync(p => p.Id == food!.FoodTypeId))!.Name;

            return viewMap6;
        }

        public async Task<FoodViewModel> UpdateAsync(int id, FoodForCreationDto model)
        {
            var existType = await _foodRepository.GetAsync(type => type.Id == id);

            if (existType is null)
            {
                throw new Exception("Food not found!");
            }

            var mappedFood = _mapper.Map(model, existType);
            mappedFood.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
            _foodRepository.Update(mappedFood);
            await _appDbContext.SaveChangesAsync();

            var foodView = _mapper.Map<FoodViewModel>(mappedFood);
            foodView.FoodTypeName = (await _foodTypeRepository.GetAsync(p => p.Id == mappedFood.FoodTypeId))!.Name;

            return foodView;
        }
    }
}
