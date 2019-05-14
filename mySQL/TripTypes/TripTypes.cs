using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL
{
    public class TripTypes
    {
        public TripTypes() { }

        public string TripTypeId { get; set; }
        public string TTName { get; set; }

        // makes identival copy of Customer
        public TripTypes Clone()
        {
            TripTypes copy = new TripTypes();
            copy.TripTypeId = this.TripTypeId;
            copy.TTName = this.TTName;
            return copy;
        }
    }
}
