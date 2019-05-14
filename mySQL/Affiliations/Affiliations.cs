using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Affiliations
{
    public class Affiliations
    {
        public Affiliations() { }

        public string AffilitationId { get; set; }

        public string AffName { get; set; }

        public string AffDesc { get; set; }

        // makes identival copy of Customer
        public Affiliations Clone()
        {
            Affiliations copy = new Affiliations();
            copy.AffilitationId = this.AffilitationId;
            copy.AffName = this.AffName;
            copy.AffDesc = this.AffDesc;
            return copy;
        }
    }
}
