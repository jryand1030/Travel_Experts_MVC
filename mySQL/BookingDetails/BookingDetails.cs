using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.BookingDetails
{
    public class BookingDetails
    {
        public BookingDetails() { }

        public int BookingDetailId { get; set; }
        public float ItineraryNo { get; set; }
        public DateTime? TripStart { get; set; }
        public DateTime? TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public decimal BasePrice { get; set; }
        public decimal AgencyCommission { get; set; }
        public int BookingId { get; set; }
        public string RegionId { get; set; }
        public string ClassId { get; set; }
        public string FeeId { get; set; }
        public int ProductSupplierId { get; set; }

        // makes identival copy of Customer
        public BookingDetails Clone()
        {
            BookingDetails copy = new BookingDetails();
            copy.BookingDetailId = this.BookingDetailId;
            copy.ItineraryNo = this.ItineraryNo;
            copy.TripStart = this.TripStart;
            copy.TripEnd = this.TripEnd;
            copy.Description = this.Description;
            copy.Destination = this.Destination;
            copy.BasePrice = this.BasePrice;
            copy.AgencyCommission = this.AgencyCommission;
            copy.BookingId = this.BookingId;
            copy.RegionId = this.RegionId;
            copy.ClassId = this.ClassId;
            copy.FeeId = this.FeeId;
            copy.ProductSupplierId = this.ProductSupplierId;
            return copy;
        }
    }
}
