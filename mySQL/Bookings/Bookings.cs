using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Bookings
{
    public class Bookings
    {
        public Bookings() { }

        public int BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string BookingNo { get; set; }
        public float TravelerCount { get; set; }
        public int CustomerId { get; set; }
        public string TripTypeId { get; set; }
        public int? PackageId { get; set; }

        // makes identival copy of Customer
        public Bookings Clone()
        {
            Bookings copy = new Bookings();
            copy.BookingId = this.BookingId;
            copy.BookingDate = this.BookingDate;
            copy.BookingNo = this.BookingNo;
            copy.TravelerCount = this.TravelerCount;
            copy.CustomerId = this.CustomerId;
            copy.TripTypeId = this.TripTypeId;
            copy.PackageId = this.PackageId;
            return copy;
        }
    }
}
