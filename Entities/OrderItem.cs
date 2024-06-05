using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities;

public partial class OrderItem
{
    public short OrderItemId { get; set; }

    public short? ProdId { get; set; }

    public short? OrderId { get; set; }

    public short? Quantity { get; set; }
    
    public virtual Order? Order { get; set; }
    
    public virtual Product? Prod { get; set; }
}
