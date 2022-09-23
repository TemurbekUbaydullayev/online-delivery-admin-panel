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
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly AppDbContext _appDbContext;

        public OrderService()
        {
            _mapper = new Mapper(new MapperConfiguration(p => p.AddProfile<MapperProfile>()));
            _appDbContext = new AppDbContext();
            _orderRepository = new GenericRepository<Order>(_appDbContext);
            _customerRepository = new GenericRepository<Customer>(_appDbContext);
        }
        public async Task<OrderViewModel> CreateAsync(OrderForCreationDto model)
        {
            var anyCustomer = await _customerRepository.GetAsync(p => p.Id == model.CustomerId);

            if (model.CustomerId != 0 || model.CustomerId != null)
            {
                if (anyCustomer is null)
                    throw new Exception("Customer not found!");
            }

            var mappedOrder = _mapper.Map<Order>(model);
            mappedOrder.CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
            mappedOrder.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

            var resOrder = (await _orderRepository.CreateAsync(mappedOrder)).Entity;
            await _appDbContext.SaveChangesAsync();

            var viewOrder = _mapper.Map<OrderViewModel>(mappedOrder);

            viewOrder.CustomerName = anyCustomer!.FirstName + " " + anyCustomer.LastName;


            return viewOrder;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            var order = _orderRepository.Where(expression);

            if (!order.Any())
            {
                throw new Exception("Order not found!");
            }

            _orderRepository.DeleteRange(order);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null,
            PaginationParameters? parameters = null)
        {
            var orders = _orderRepository.Where(expression).Include(order => order.Customer).ToPagedList(parameters);

            var viewMaps = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var ord = _mapper.Map<OrderViewModel>(order);
                ord.CustomerName = order.Customer!.FirstName + " " + order.Customer!.LastName;
                //ord.Phone = (await _customerRepository.GetAsync(p => p.Id == order!.CustomerId))!.PhoneNumber;
                
                viewMaps.Add(ord);
            }

            return viewMaps;

        }

        public async Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var order = await _orderRepository.GetAsync(expression);

            if (order is null)
            {
                throw new Exception("Order not found!");
            }

            var viewMap = _mapper.Map<OrderViewModel>(order);
            var viewName = (await _customerRepository.GetAsync(p => p.Id == order!.CustomerId))!;
            viewMap.CustomerName = viewName.FirstName + " " + viewName.LastName;
            //viewMap.Phone = (await _customerRepository.GetAsync(p => p.Id == order!.CustomerId))!.PhoneNumber;

            return viewMap;
        }

        public async Task<OrderViewModel> UpdateAsync(int id, OrderForCreationDto model)
        {
            var existOrder = await _orderRepository.GetAsync(type => type.Id == id);

            if (existOrder is null)
            {
                throw new Exception("Order not found!");
            }

            var mappedOrder = _mapper.Map(model, existOrder);
            mappedOrder.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
            _orderRepository.Update(mappedOrder);
            await _appDbContext.SaveChangesAsync();

            var orderView = _mapper.Map<OrderViewModel>(mappedOrder);
            var orderName = (await _customerRepository.GetAsync(p => p.Id == mappedOrder.CustomerId))!;
            orderView.CustomerName = orderName.FirstName + " " + orderName.LastName;

            return orderView;
        }
    }
}
