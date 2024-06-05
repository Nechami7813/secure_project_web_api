using Entities;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.order;
using Repository.product;
using Service.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zxcvbn;

namespace Service;

public class OrderService : IOrderService
{
    ILogger<OrderService> _logger;
    IOrderRepository _orderRepository;
    IProductService _productService;
    public OrderService(IProductService productService, ILogger<OrderService> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _productService = productService;
    }
    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _orderRepository.Get();
    }
    public async Task<CustomHttpResponse<Order>> AddOrder(Order order)
    {
        var products = _productService.GetProducts(0, 1000, "", "", "");
        List<Product> productList = (List<Product>)await products;
        var totalSum = order.OrderItems
                .Where(oi => productList.Any(p => p.ProdId == oi.ProdId))
                .Sum(oi => productList.First(p => p.ProdId == oi.ProdId).Price * oi.Quantity);

        Console.Write("  totalSum  " + totalSum);
        Console.Write("  order.OrderSum  " + order.OrderSum);

        if (order.OrderSum != totalSum)
        {
            Console.WriteLine("ERR");
            _logger.LogError("🏴‍☠️🏴‍☠️🏴‍☠️🏴‍☠️🏴‍☠️🏴‍☠!!!התראה🏴‍☠️🏴‍☠️🏴‍☠️🏴‍☠️🏴‍☠️🏴‍☠️\n חדירת גנב לחנות, קרא למשטרה להזכירך 📞 100 ");

            return new CustomHttpResponse<Order>(null, 401); // Return 401 status code with null Order
        }
        else
        {
            Order result = await _orderRepository.AddOrder(order);
            return result != null ? new CustomHttpResponse<Order>(result, 200) : new CustomHttpResponse<Order>(null, 401);
        }
    }
}

