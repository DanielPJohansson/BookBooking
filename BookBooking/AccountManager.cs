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
        public static UserAccount CurrentUser { get; set; }

        public UserAccount CreateCustomerAccount()
        {
            return new CustomerAccount("", "", "", "");
        }

        public UserAccount CreateAdministratorAccount()
        {
            UserAccount administrator = new AdministratorAccount("", "", "", "");
            return administrator;
        }

        public string GenerateUserName()
        {
            throw new NotImplementedException();
        }
    }
}
