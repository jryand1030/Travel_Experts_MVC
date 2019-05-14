using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL
{
    public class Products
    {
        public Products() { }

        public int ProductID { get; set; }
        public string ProdName { get; set; }

        // makes identival copy of Customer
        public Products Clone()
        {
            Products copy = new Products();
            copy.ProductID = this.ProductID;
            copy.ProdName = this.ProdName;
            return copy;
        }
    }
}
