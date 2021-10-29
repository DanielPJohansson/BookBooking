using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    class CustomerAccount: UserAccount
    {
        public List<Book> CurrentlyLendedBooks { get; set; }

        public CustomerAccount(string firstName, string lastName, string userName, string Password)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
