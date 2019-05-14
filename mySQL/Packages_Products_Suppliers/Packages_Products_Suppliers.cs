using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Packages_Products_Suppliers
{
    public class Packages_Products_Suppliers
    {
        public Packages_Products_Suppliers() { }

        public int PackageId { get; set; }
        public int ProductSupplierId { get; set; }

        // makes identival copy of Customer
        public Packages_Products_Suppliers Clone()
        {
            Packages_Products_Suppliers copy = new Packages_Products_Suppliers();
            copy.PackageId = this.PackageId;
            copy.ProductSupplierId = this.ProductSupplierId;
            return copy;
        }
    }
}
