using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.product;

public class ProductRepository : IProductRepository
{
    Product326075108Context productContext;
    public ProductRepository(Product326075108Context product326075108Context)
    {
        productContext = product326075108Context;
    }

    public async Task<IEnumerable<Product>> Get(int? min, int? max, string? name, string? desc, int?[] categoriesIds)
    {
        var query = productContext.Products.Include(p => p.Category).Where(product =>
        (desc == null ? (true) : (product.Description.Contains(desc)))
        && ((min == null) ? (true) : (product.Price >= min))
        && ((max == null) ? (true) : (product.Price <= max))
        && ((categoriesIds.Length == 0) ? (true) : (categoriesIds.Contains(product.CategoryId))))
            .OrderBy(product => product.Price);
        //.Skip((position - 1) * skip)
        //.Take(skip);
        await Console.Out.WriteLineAsync(query.ToQueryString());
        List<Product> products = await query.ToListAsync();
        return products;


    }
}
