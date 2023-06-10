using System;
using System.Collections.Generic;

namespace AS.DATA.EF.Models
{
    public partial class OrderProduct
    {
        public int OrderProductId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public short OrderQuantity { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
