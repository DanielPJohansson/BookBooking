using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class LoanManager
    {
        public CustomerAccount CurrentUser { get; set; }

        public LoanManager(CustomerAccount currentUser)
        {
            CurrentUser = currentUser;
        }

        public void Borrow(ILendable borrowedItem)
        {
            if (borrowedItem.CurrentlyBorrowedBy == null)
            {
                CurrentUser.CurrentLoans.Add(borrowedItem);
                borrowedItem.CurrentlyBorrowedBy = CurrentUser;
            }
        }

        public void Return(ILendable borrowedItem)
        {
            CurrentUser.CurrentLoans.Remove(borrowedItem);
            borrowedItem.CurrentlyBorrowedBy = null;
        }
    }
}
