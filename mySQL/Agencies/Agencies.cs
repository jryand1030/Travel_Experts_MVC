using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Agencies
{
    public class Agencies
    {
        public Agencies() { }

        public int AgencyId { get; set; }
        public string AgncyAddress { get; set; }
        public string AgncyCity { get; set; }
        public string AgncyProv { get; set; }
        public string AgncyPostal { get; set; }
        public string AgncyCountry { get; set; }
        public string AgncyPhone { get; set; }
        public string AgncyFax { get; set; }

        // makes identival copy of Customer
        public Agencies Clone()
        {
            Agencies copy = new Agencies();
            copy.AgencyId = this.AgencyId;
            copy.AgncyAddress = this.AgncyAddress;
            copy.AgncyCity = this.AgncyCity;
            copy.AgncyProv = this.AgncyProv;
            copy.AgncyPostal = this.AgncyPostal;
            copy.AgncyCountry = this.AgncyCountry;
            copy.AgncyPhone = this.AgncyPhone;
            copy.AgncyFax = this.AgncyFax;
            return copy;
        }
    }
}
