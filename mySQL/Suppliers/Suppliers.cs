using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Suppliers
{
    public class Suppliers
    {
        public Suppliers() { }

        public int SupplierId { get; set; }
        public string SupName { get; set; }

        // makes identival copy of Customer
        public Suppliers Clone()
        {
            Suppliers copy = new Suppliers();
            copy.SupplierId = this.SupplierId;
            copy.SupName = this.SupName;
            return copy;
        }
    }
}
