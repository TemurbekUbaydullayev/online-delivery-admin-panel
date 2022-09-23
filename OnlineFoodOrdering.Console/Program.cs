using OnlineFoodOrdering.Data.DbContexts;
using OnlineFoodOrdering.Data.Interfaces.Customers;
using OnlineFoodOrdering.Data.Repositories.Customers;
using OnlineFoodOrdering.Service.DTOs.Customers;
using OnlineFoodOrdering.Service.Interfaces;
using OnlineFoodOrdering.Service.Services;
using System;

public class Program 
{
    static async Task Main(string[] args) 
    {
        AppDbContext appDbContex = new AppDbContext();

        CustomerForCreationDto customerForCreationDto = new CustomerForCreationDto();
        //{
        //    FirstName = "Shahriyor",
        //    LastName = "Abduhalim o'g'li",
        //    BirthDate = DateOnly.Parse("10/02/1993"),
        //    Email = "bozorov12@gmail.com",
        //    PhoneNumber = "+998900000000",
        //    Login = "bbbb",
        //    Password = "aaaa"
        //};

        ICustomerService customerService = new CustomerService();
        //await customerService.CreateAsync(customerForCreationDto);

        //await customerService.DeleteAsync(customerForCreationDto => customerForCreationDto.Id == 1);

        //await customerService.UpdateAsync(2, customerForCreationDto);

        var res = await customerService.GetAllAsync();

        foreach (var customer in res)
        {
            Console.WriteLine(customer.PhoneNumber);
        }
        
       

        



    }
}