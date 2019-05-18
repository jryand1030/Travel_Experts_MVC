using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.SupplierContacts
{
    public class SupplierContacts
    {
        public SupplierContacts() { }

        public int SupplierContactId { get; set; }
        public string SupConFirstName { get; set; }
        public string SupConLastName { get; set; }
        public string SupConCompany { get; set; }
        public string SupConAddress { get; set; }
        public string SupConCity { get; set; }
        public string SupConProv { get; set; }
        public string SupConPostal { get; set; }
        public string SupConCountry { get; set; }
        public string SupConBusPhone { get; set; }
        public string SupConFax { get; set; }
        public string SupConEmail { get; set; }
        public string SupConURL { get; set; }
        public string AffiliationId { get; set; }
        public int SupplierId { get; set; }

        // makes identival copy of Customer
        public SupplierContacts Clone()
        {
            SupplierContacts copy = new SupplierContacts();
            copy.SupplierContactId = this.SupplierContactId;
            copy.SupConFirstName = this.SupConFirstName;
            copy.SupConLastName = this.SupConLastName;
            copy.SupConCompany = this.SupConCompany;
            copy.SupConAddress = this.SupConAddress;
            copy.SupConCity = this.SupConCity;
            copy.SupConProv = this.SupConProv;
            copy.SupConPostal = this.SupConPostal;
            copy.SupConCountry = this.SupConCountry;
            copy.SupConBusPhone = this.SupConBusPhone;
            copy.SupConFax = this.SupConFax;
            copy.SupConEmail = this.SupConEmail;
            copy.SupConURL = this.SupConURL;
            copy.AffiliationId = this.AffiliationId;
            copy.SupplierId = this.SupplierId;
            return copy;
        }
    }
}
