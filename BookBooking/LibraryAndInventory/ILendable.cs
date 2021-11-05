using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public interface ILendable : IListableAsMenu
    {
        public string Title { get; set; }
        public DateTime DateOfLoan { get; set; }
        public DateTime LastReturnDate { get; set; }
        public IAccount CurrentlyBorrowedBy { get; set; }

        
    }
}

