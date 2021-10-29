using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class AccountManager
    {
        public static List<UserAccount> UserAccounts { get; set; }

        public UserAccount CreateCustomerAccount()
        {
            return new UserAccount("", "", "", "");
        }

        public UserAccount CreateAdministratorAccount()
        {
            UserAccount administrator = new UserAccount("", "", "", "");
            administrator.IsAdmin = true;
            return administrator;
        }

        public string GenerateUserName()
        {
            throw new NotImplementedException();
        }
    }
}
