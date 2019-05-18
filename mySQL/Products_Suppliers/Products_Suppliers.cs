using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Products_Suppliers
{
    public class Products_Suppliers
    {
        public Products_Suppliers() { }

        public int ProductSupplierId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }

        // makes identival copy of Customer
        public Products_Suppliers Clone()
        {
            Products_Suppliers copy = new Products_Suppliers();
            copy.ProductSupplierId = this.ProductSupplierId;
            copy.ProductId = this.ProductId;
            copy.SupplierId = this.SupplierId;
            return copy;
        }
    }
}
