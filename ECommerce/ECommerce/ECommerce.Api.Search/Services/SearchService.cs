using ECommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService

    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await orderService.GetOrdersAsync(customerId);
            var ProductResult = await productService.GetProductsAsync();
            var customerResult = await customerService.GetCustomerAsync(customerId);
            if (orderResult.IsSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = ProductResult.IsSuccess? ProductResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name : "Product information is not available at this time";
                    }
                }
                var result = new
                {
                    Customer = customerResult.IsSuccess ? customerResult.Customer : new {Name = "Customer information not available"},
                    Orders = orderResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
