using Entities;
using Repository.category;
using Repository.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.category;

public class CategoryService : ICategoryService
{

    ICategoryRepository categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await categoryRepository.Get();
    }
}
