﻿using ECommerce.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string Message)> GetOrdersAsync();
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string Message)> GetOrdersAsync(int id);

    }
}
