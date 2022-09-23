using AutoMapper;
using OnlineFoodOrdering.Data.DbContexts;
using OnlineFoodOrdering.Data.Interfaces;
using OnlineFoodOrdering.Data.Repositories;
using OnlineFoodOrdering.Domain.Common;
using OnlineFoodOrdering.Domain.Entities;
using OnlineFoodOrdering.Service.DTOs.Customers;
using OnlineFoodOrdering.Service.Extensions;
using OnlineFoodOrdering.Service.Interfaces.Customers;
using OnlineFoodOrdering.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly AppDbContext _appDbContext;

        public CustomerService()
        {
            _appDbContext = new AppDbContext();
            _customerRepository = new GenericRepository<Customer>(_appDbContext);
            _mapper = new Mapper(new MapperConfiguration(p => p.AddProfile<MapperProfile>()));
        }
        public async Task<Customer> CreateAsync(CustomerForCreationDto model)
        {
            var entity = _mapper.Map<Customer>(model);
            entity.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
            entity.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            var res = (await _customerRepository.CreateAsync(entity)).Entity;
            await _appDbContext.SaveChangesAsync();

            return res;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Customer, bool>> expression)
        {
            var customer = _customerRepository.Where(expression);

            if (!customer.Any())
            {
                throw new Exception("Customer not found!");
            }

            _customerRepository.DeleteRange(customer);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public Task<IEnumerable<Customer>> GetAllAsync(Expression<Func<Customer, bool>>? expression = null, PaginationParameters? parameters = null)
            => Task.FromResult<IEnumerable<Customer>>(_customerRepository.Where(expression).ToPagedList(parameters));
        public Task<Customer?> GetAsync(Expression<Func<Customer, bool>> expression)
            => _customerRepository.GetAsync(expression);

        public async Task<Customer> UpdateAsync(int id, CustomerForCreationDto model)
        {
            var existCustomer = await _customerRepository.GetAsync(p => p.Id == id);

            if (existCustomer is null)
            {
                throw new Exception("Customer not found!");
            }

            var customMapped = _mapper.Map(model, existCustomer);
            customMapped.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            _customerRepository.Update(customMapped);
            await _appDbContext.SaveChangesAsync();

            return customMapped;
        }
    }
}
