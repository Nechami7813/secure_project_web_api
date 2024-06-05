using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.order;

public class OrderRepository : IOrderRepository
{


    Product326075108Context productContext;
    public OrderRepository(Product326075108Context product326075108Context)
    {
        productContext = product326075108Context;
    }
    public async Task<IEnumerable<Order>> Get()
    {
        return await productContext.Orders.ToListAsync();

    }

    public async Task<Order> AddOrder(Order order)
    {
        await productContext.Orders.AddAsync(order);
        await productContext.SaveChangesAsync();
        return order;

    }
}
