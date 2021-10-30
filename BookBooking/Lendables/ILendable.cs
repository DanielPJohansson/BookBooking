using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public interface ILendable : IMenuItem
    {
        public UserAccount CurrentlyBorrowedBy { get; set; }
    }
}
