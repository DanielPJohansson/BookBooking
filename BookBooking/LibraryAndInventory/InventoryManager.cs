using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class InventoryManager
    {
        public UserSession Session { get; set; }

        public InventoryManager(UserSession currentSession)
        {
            Session = currentSession;
        }

        public void Borrow(ILendable borrowedItem)
        {
            if (borrowedItem.CurrentlyBorrowedBy == null)
            {
                borrowedItem.CurrentlyBorrowedBy = Session.User;
                borrowedItem.DateOfLoan = DateTime.Now;
                borrowedItem.LastReturnDate = borrowedItem.DateOfLoan.AddDays(14);
            }
        }

        public void Return(ILendable borrowedItem)
        {
            borrowedItem.CurrentlyBorrowedBy = null;
        }

        public void RemoveLendable(ILendable lendable)
        {
            if (lendable.CurrentlyBorrowedBy != null)
            {
                Return(lendable);
            }
            Session.Library.LendablesInInventory.Remove(lendable);
            Session.Library.LendablesRemovedFromInventory.Add(lendable);
        }

        public Book CreateNewBook()
        {
            UIRenderer.ResetScreen();
            int yPos = 3;
            string title = InputManager.RequestFreeInput("Bokens titel: ", yPos);
            string authorFirstName = InputManager.RequestNameInput("Författarens förnamn: ", ++yPos);
            string authorLastName = InputManager.RequestNameInput("Författarens efternamn: ", ++yPos);
            int yearPublished = InputManager.RequestYearInput("Utgivningsår: ", ++yPos);

            UIRenderer.DisplayText("Ny bok tillagd. Tryck på valfri tangent för att fortsätta", yPos: yPos + 2, maxWidth: 100);
            Console.ReadKey();
            UIRenderer.ResetScreen();

            return new Book(title, authorFirstName, authorLastName, yearPublished);
        }
    }
}
