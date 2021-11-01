using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    class AdministratorAccount : BaseUserAccount
    {
        public AdministratorAccount(string firstName, string lastName, string userName, string password) : base(firstName, lastName, userName, password)
        {
            IsAdmin = true;
        }
    }
}
