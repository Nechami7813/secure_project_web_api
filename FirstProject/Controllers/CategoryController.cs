using Entities;
using Microsoft.AspNetCore.Mvc;
using Service.category;
using Service.product;

namespace FirstProject.Controllers;
[ApiController]

[Route("api/[controller]")]


public class CategoryController : ControllerBase
{
    ICategoryService categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var categories = await categoryService.GetCategories();
        if (categories.Count() > 0)
            return Ok(categories);
        return NoContent();
    }
}
