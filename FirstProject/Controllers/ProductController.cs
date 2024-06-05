using AutoMapper;
using DTOs;
using Entities;
using Entities.product;
using Microsoft.AspNetCore.Mvc;
using Service.product;
using Service.user;
using System.Collections.Generic;

namespace FirstProject.Controllers;
[ApiController]

[Route("api/[controller]")]


public class ProductController : ControllerBase
{
     IProductService productService;
    
    private IMapper mapper;

    public ProductController(IProductService productService, IMapper mapper)
    {
        this.productService = productService;
        this.mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get(int? min, int? max, string? name, string? descripition, string? categoriesIds)
    {
        var products = await productService.GetProducts( min,  max,  name,  descripition, categoriesIds);
        var productsDto = mapper.Map<List<Product>, List<ProductDto>>((List<Product>)products);
        if (productsDto.Count() > 0)
        {

            return Ok(productsDto);
        }
            
        return NoContent();
    }

}
