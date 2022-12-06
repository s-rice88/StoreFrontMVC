using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class ClassLocation
    {
        public ClassLocation()
        {
            Products = new HashSet<Product>();
        }

        public int LocationId { get; set; }
        public string CampusName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
