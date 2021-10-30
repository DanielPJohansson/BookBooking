using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public abstract class Lendable
    {
        public UserAccount CurrentlyBorrowedBy { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime LastReturnDate { get; set; }
    }
}
