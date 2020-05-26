using ECommerce.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string Message)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer Customer, string Message)> GetCustomersAsync(int id);

    }
}
