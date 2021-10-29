using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    class AdministratorAccount : UserAccount
    {
        public AdministratorAccount(string firstName, string lastName, string userName, string Password)
        {
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = true;
        }
    }
}
