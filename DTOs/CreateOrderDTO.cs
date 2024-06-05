using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs;

public class CreateOrderDTO
{
    public short? UserId { get; set; }
    public short? OrderSum { get; set; }
    public virtual ICollection<OrderItemDto> OrderItemDTOs { get; set; } = new List<OrderItemDto>();

}
