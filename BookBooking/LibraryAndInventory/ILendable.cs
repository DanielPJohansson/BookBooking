using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public interface ILendable : IMenuItem
    {
        public BaseUserAccount CurrentlyBorrowedBy { get; set; }
        //public DateTime StartTimeOfLoan { get; set; }
        //public DateTime LastReturnDate { get; set; }
    }
}
