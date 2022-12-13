using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual UserDetail User { get; set; } = null!;
    }
}
