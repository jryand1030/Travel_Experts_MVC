using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Customers
{
    public class Customers
    {
        public Customers() { }

        public int CustomerId { get; set; }
        public string CustFirstName { get; set; }
        public string CustLastName { get; set; }
        public string CustAddress { get; set; }
        public string CustCity { get; set; }
        public string CustProv { get; set; }
        public string CustPostal { get; set; }
        public string CustCountry { get; set; }
        public string CustHomePhone { get; set; }
        public string CustBusPhone { get; set; }
        public string CustEmail { get; set; }
        public int AgentId { get; set; }

        // makes identival copy of Customer
        public Customers Clone()
        {
            Customers copy = new Customers();
            copy.CustomerId = this.CustomerId;
            copy.CustFirstName = this.CustFirstName;
            copy.CustLastName = this.CustLastName;
            copy.CustAddress = this.CustAddress;
            copy.CustCity = this.CustCity;
            copy.CustProv = this.CustProv;
            copy.CustPostal = this.CustPostal;
            copy.CustCountry = this.CustCountry;
            copy.CustHomePhone = this.CustHomePhone;
            copy.CustBusPhone = this.CustBusPhone;
            copy.CustEmail = this.CustEmail;
            copy.AgentId = this.AgentId;
            return copy;
        }
    }
}
