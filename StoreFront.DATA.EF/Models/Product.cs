using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public bool Discontinued { get; set; }
        public string? Description { get; set; }
        public string? ProductImage { get; set; }
        public string ProductName { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }
        public bool IsOnline { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ClassLocation? Location { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
