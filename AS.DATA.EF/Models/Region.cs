using System;
using System.Collections.Generic;

namespace AS.DATA.EF.Models
{
    public partial class Region
    {
        public Region()
        {
            Countries = new HashSet<Country>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; } = null!;

        public virtual ICollection<Country> Countries { get; set; }
    }
}
