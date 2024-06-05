using Entities;
using Repository.product;
using Repository.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.product;

public class ProductService : IProductService
{

    IProductRepository productRepository;
    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    public async Task<IEnumerable<Product>> GetProducts(int? min, int? max, string? name, string? descripition, string? categoriesIds)
    {
       
        var stringCategories = categoriesIds?.Split(" ");
        if (stringCategories?.Length > 0 && categoriesIds!="") { 
        var categories=new int?[stringCategories.Length];
        int i = 0;

        foreach (var item in stringCategories)
        {
            categories[i++] =Convert.ToInt32(item);
        }
            return await productRepository.Get(min, max, name, descripition, categories);
    }
        return await productRepository.Get(min, max, name, descripition, []);
}
}
