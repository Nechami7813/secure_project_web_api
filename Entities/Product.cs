using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities;

public partial class Product
{
    public short ProdId { get; set; }

    public string? ProdName { get; set; }

    public short? Price { get; set; }

    public short? CategoryId { get; set; }

    public string? Description { get; set; }
    public virtual Category? Category { get; set; }
  
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
