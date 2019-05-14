using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Fees
{
    public class Fees
    {
        public Fees() { }

        public string FeeId { get; set; }
        public string FeeName { get; set; }
        public decimal FeeAmt { get; set; }
        public string FeeDesc { get; set; }

        // makes identival copy of Customer
        public Fees Clone()
        {
            Fees copy = new Fees();
            copy.FeeId = this.FeeId;
            copy.FeeName = this.FeeName;
            copy.FeeAmt = this.FeeAmt;
            copy.FeeDesc = this.FeeDesc;
            return copy;
        }
    }
}
