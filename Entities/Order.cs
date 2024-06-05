using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities;

public partial class Order
{
    public short OrderId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public short? OrderSum { get; set; }

    public short? UserId { get; set; }
    
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public virtual User? User { get; set; }
}
