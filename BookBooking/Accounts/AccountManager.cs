using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class AccountManager
    {
        public List<BaseUserAccount> UserAccounts { get; set; }

        public BaseUserAccount CreateCustomerAccount()
        {
            return new BaseUserAccount("", "", "", "");
        }

        public BaseUserAccount CreateAdministratorAccount()
        {
            BaseUserAccount administrator = new AdministratorAccount("", "", "", "");
            return administrator;
        }

        public string GenerateUserName()
        {
            throw new NotImplementedException();
        }
    }
}
