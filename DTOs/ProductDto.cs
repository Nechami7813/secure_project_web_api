using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.product;

public class ProductDto
{
    public short ProdId { get; set; }
    public string? ProdName { get; set; }

    public short? Price { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }
}
