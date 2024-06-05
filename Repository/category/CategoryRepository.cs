using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.category;

public class CategoryRepository : ICategoryRepository
{
    Product326075108Context productContext;
    public CategoryRepository(Product326075108Context product326075108Context)
    {
        productContext = product326075108Context;
    }

    public async Task<IEnumerable<Category>> Get()
    {
        try
        {
            return await productContext.Categories.ToListAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}
