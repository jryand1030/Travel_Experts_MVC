using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Rewards
{
    public class Rewards
    {
        public Rewards() { }

        public int RewardId { get; set; }
        public string RwdName { get; set; }
        public string RwdDesc { get; set; }

        // makes identival copy of Customer
        public Rewards Clone()
        {
            Rewards copy = new Rewards();
            copy.RewardId = this.RewardId;
            copy.RwdName = this.RwdName;
            copy.RwdDesc = this.RwdDesc;
            return copy;
        }
    }
}
