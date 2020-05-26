﻿using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Jessica Smith", Address = "20 Elm St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "John Smith", Address = "30 Main St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Alam Sleeper", Address = "10 Main 10th St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "William Johnson", Address = "100 10th St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 5, Name = "William Talbert", Address = "121 9th St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 6, Name = "Scott Johnson", Address = "10 5th St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 7, Name = "Audery Charity", Address = "30 Elm  St." });
                dbContext.SaveChanges();
            }
        }

        public async  Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string Message)> GetCustomersAsync()
        {
            logger.LogInformation("GetCustomers..");
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string Message)> GetCustomersAsync(int id)
        {
            logger.LogInformation("GetCustomers by Id..");
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c=>c.Id == id);
                if (customer != null )
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
