using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class LendableManager
    {
        public UserSession Session { get; set; }

        public LendableManager(UserSession currentSession)
        {
            Session = currentSession;
        }

        public void Borrow(ILendable borrowedItem)
        {
            if (borrowedItem.CurrentlyBorrowedBy == null)
            {
                Session.User.CurrentLoans.Add(borrowedItem);
                borrowedItem.CurrentlyBorrowedBy = Session.User;
            }
        }

        public void Return(ILendable borrowedItem)
        {
            borrowedItem.CurrentlyBorrowedBy.CurrentLoans.Remove(borrowedItem);
            borrowedItem.CurrentlyBorrowedBy = null;
        }

        public void RemoveLendable(ILendable lendable)
        {
            Return(lendable);
            Session.Library.LendablesInInventory.Remove(lendable);
            Session.Library.LendablesRemovedFromInventory.Add(lendable);
        }
    }
}
