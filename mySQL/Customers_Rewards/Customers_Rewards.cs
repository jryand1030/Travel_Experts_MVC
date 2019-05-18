using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Customers_Rewards
{
    public class Customers_Rewards
    {
        public Customers_Rewards() { }

        public int CustomerId { get; set; }
        public int RewardId { get; set; }
        public string RwdNumber { get; set; }

        // makes identival copy of Customer
        public Customers_Rewards Clone()
        {
            Customers_Rewards copy = new Customers_Rewards();
            copy.CustomerId = this.CustomerId;
            copy.RewardId = this.RewardId;
            copy.RwdNumber = this.RwdNumber;
            return copy;
        }
    }
}
