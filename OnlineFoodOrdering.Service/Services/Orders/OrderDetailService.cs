using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineFoodOrdering.Data.DbContexts;
using OnlineFoodOrdering.Data.Interfaces;
using OnlineFoodOrdering.Data.Repositories;
using OnlineFoodOrdering.Domain.Common;
using OnlineFoodOrdering.Domain.Entities;
using OnlineFoodOrdering.Service.DTOs.Orders;
using OnlineFoodOrdering.Service.Extensions;
using OnlineFoodOrdering.Service.Interfaces.Orders;
using OnlineFoodOrdering.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.Services.Orders
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Food> _foodRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public OrderDetailService()
        {
            _appDbContext = new AppDbContext();
            _mapper = new Mapper(new MapperConfiguration(p => p.AddProfile<MapperProfile>()));
            _foodRepository = new GenericRepository<Food>(_appDbContext);
            _orderRepository = new GenericRepository<Order>(_appDbContext);
            _orderDetailRepository = new GenericRepository<OrderDetail>(_appDbContext);
        }
        public async Task<OrderDetailViewModel> CreateAsync(OrderDetailForCreationDto model)
        {
            var anyOrder = await _orderRepository.GetAsync(p => p.Id == model.OrderId);
            var anyFood = await _foodRepository.GetAsync(p => p.Id == model.FoodId);

            if (model.OrderId != 0 || model.OrderId != null || model.FoodId != 0 || model.FoodId != null)
                if (anyOrder is null || anyFood is null)
                    throw new Exception("Order or Food not found!");

            var mappedOD = _mapper.Map<OrderDetail>(model);
            mappedOD.TotalPrice = anyFood!.Price * mappedOD.Quantity;
            var resCreateOrder = await _orderDetailRepository.CreateAsync(mappedOD);
            await _appDbContext.SaveChangesAsync();

            var viewOD = _mapper.Map<OrderDetailViewModel>(mappedOD);
            viewOD.FoodName = anyFood!.Name;

            return viewOD;
        }

        public async Task<bool> DeleteAsync(Expression<Func<OrderDetail, bool>> expression)
        {
            var orderDetail = _orderDetailRepository.Where(expression);

            if (!orderDetail.Any())
            {
                throw new Exception("Order not found!");
            }

            _orderDetailRepository.DeleteRange(orderDetail);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetAllAsync(Expression<Func<OrderDetail, bool>>? expression = null, PaginationParameters? parameters = null)
        {
            var orderODs = _orderDetailRepository.Where(expression).Include(order => order.Food).ToPagedList(parameters);


            var viewMaps = new List<OrderDetailViewModel>();

            foreach (var orderOD in orderODs)
            {
                var ord = _mapper.Map<OrderDetailViewModel>(orderOD);
                ord.FoodName = orderOD.Food!.Name;

                ord.OrderId = orderOD.OrderId;
                

                viewMaps.Add(ord);
            }

            return viewMaps;
        }

        //public async Task<IEnumerable<OrderDetailViewModel>> GetAllAsyncOD(long id)
        //{
        //    var orderODs = _orderDetailRepository.Where(expression).Include(order => order.Food).ToPagedList(parameters);

        //    var viewMaps = new List<OrderDetailViewModel>();

        //    foreach (var orderOD in orderODs)
        //    {
        //        var ord = _mapper.Map<OrderDetailViewModel>(orderOD);
        //        ord.FoodName = orderOD.Food!.Name;


        //        viewMaps.Add(ord);
        //    }

        //    return viewMaps;
        //}
        public async Task<OrderDetailViewModel?> GetAsync(Expression<Func<OrderDetail, bool>> expression)
        {
            var orderDetail = await _orderDetailRepository.GetAsync(expression);

            if (orderDetail is null)
            {
                throw new Exception("Order not found!");
            }

            var viewMap = _mapper.Map<OrderDetailViewModel>(orderDetail);
            var viewName = (await _foodRepository.GetAsync(p => p.Id == orderDetail!.FoodId))!;
            viewMap.FoodName = viewName.Name;

            return viewMap;
        }

        public async Task<OrderDetailViewModel> UpdateAsync(int id, OrderDetailForCreationDto model)
        {
            var existOrderDetail = await _orderDetailRepository.GetAsync(type => type.Id == id);

            if (existOrderDetail is null)
            {
                throw new Exception("Order not found!");
            }

            var mappedOrderDetail = _mapper.Map(model, existOrderDetail);
            _orderDetailRepository.Update(mappedOrderDetail);
            await _appDbContext.SaveChangesAsync();

            var viewOD = _mapper.Map<OrderDetailViewModel>(mappedOrderDetail);
            viewOD.FoodName = (await _foodRepository.GetAsync(p => p.Id == mappedOrderDetail.FoodId))!.Name;

            return viewOD;
        }
    }
}
