using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Classes
{
    public class Classes
    {
        public Classes() { }

        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDesc { get; set; }

        // makes identival copy of Customer
        public Classes Clone()
        {
            Classes copy = new Classes();
            copy.ClassId = this.ClassId;
            copy.ClassName = this.ClassName;
            copy.ClassDesc = this.ClassDesc;
            return copy;
        }
    }
}
