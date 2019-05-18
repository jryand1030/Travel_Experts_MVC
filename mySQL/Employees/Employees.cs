using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Employees
{
    public class Employees
    {
        public Employees() { }

        public string EmpFirstName { get; set; }
        public string EmpMiddleInitial { get; set; }
        public string EmpLastName { get; set; }
        public string EmpBusPhone { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPosition { get; set; }

        // makes identival copy of Customer
        public Employees Clone()
        {
            Employees copy = new Employees();
            copy.EmpFirstName = this.EmpFirstName;
            copy.EmpMiddleInitial = this.EmpMiddleInitial;
            copy.EmpLastName = this.EmpLastName;
            copy.EmpBusPhone = this.EmpBusPhone;
            copy.EmpEmail = this.EmpEmail;
            copy.EmpPosition = this.EmpPosition;
            return copy;
        }
    }
}
