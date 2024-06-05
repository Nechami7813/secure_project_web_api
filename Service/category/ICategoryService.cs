using Entities;

namespace Service.category
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}