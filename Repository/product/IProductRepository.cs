using Entities;

namespace Repository.product
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> Get(int? min, int? max, string? name, string? descripition, int?[] categoriesIds);
    }
}