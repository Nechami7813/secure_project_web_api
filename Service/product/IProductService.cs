using Entities;

namespace Service.product
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts(int? min, int? max,string ?name,string? descripition,string? categoriesIds);
    }
}