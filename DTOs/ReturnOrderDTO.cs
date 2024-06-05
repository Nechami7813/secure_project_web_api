using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DTOs;

public class ReturnOrderDTO
{
    public short? UserId { get; set; }
    public short? OrderSum { get; set; }
    public DateOnly? OrderDate { get; set; }
    public virtual ICollection<OrderItemDto> OrderItemDTOs { get; set; }
}