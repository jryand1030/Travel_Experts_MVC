using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Agents
{
    public class Agents
    {
        public Agents() { }

        public int AgentId { get; set; }
        public string AgtFirstName { get; set; }
        public string AgtMiddleInitial { get; set; }
        public string AgtLastName { get; set; }
        public string AgtBusPhone { get; set; }
        public string AgtEmail { get; set; }
        public string AgtPosition { get; set; }
        public int AgencyId { get; set; } // FK

        // makes identival copy of Customer
        public Agents Clone()
        {
            Agents copy = new Agents();
            copy.AgentId = this.AgentId;
            copy.AgtFirstName = this.AgtFirstName;
            copy.AgtMiddleInitial = this.AgtMiddleInitial;
            copy.AgtLastName = this.AgtLastName;
            copy.AgtBusPhone = this.AgtBusPhone;
            copy.AgtEmail = this.AgtEmail;
            copy.AgtPosition = this.AgtPosition;
            copy.AgencyId = this.AgencyId; // FK
            return copy;
        }
    }
}
