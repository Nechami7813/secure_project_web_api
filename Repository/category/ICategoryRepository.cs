using Entities;

namespace Repository.category
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> Get();
    }
}