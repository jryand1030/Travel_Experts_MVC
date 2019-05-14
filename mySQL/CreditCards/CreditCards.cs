using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.CreditCards
{
    public class CreditCards
    {
        public CreditCards() { }

        public int CreditCardId { get; set; }
        public string CCName { get; set; }
        public string CCNumber { get; set; }
        public DateTime CCExpiry { get; set; }
        public int CustomerId { get; set; }

        // makes identival copy of Customer
        public CreditCards Clone()
        {
            CreditCards copy = new CreditCards();
            copy.CreditCardId = this.CreditCardId;
            copy.CCName = this.CCName;
            copy.CCNumber = this.CCNumber;
            copy.CCExpiry = this.CCExpiry;
            copy.CustomerId = this.CustomerId;
            return copy;
        }
    }
}
