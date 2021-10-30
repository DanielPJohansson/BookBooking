using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class CustomerAccount: UserAccount
    {
        public List<ILendable> CurrentLoans { get; set; } = new();
        public CustomerAccount(string firstName, string lastName, string userName, string Password)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
