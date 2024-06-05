using Entities;
using Entities.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OrderItemDto
    {

        public short? ProdId { get; set; }
        public short? Quantity { get; set; }
    }
}
