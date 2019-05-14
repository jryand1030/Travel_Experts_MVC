using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Regions
{
    public class Regions
    {
        public Regions() { }

        public string RegionId { get; set; }
        public string RegionName { get; set; }

        // makes identival copy of Customer
        public Regions Clone()
        {
            Regions copy = new Regions();
            copy.RegionId = this.RegionId;
            copy.RegionName = this.RegionName;
            return copy;
        }
    }
}
