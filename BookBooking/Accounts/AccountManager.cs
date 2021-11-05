using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class AccountManager
    {
        public List<IAccount> UserAccounts { get; set; } = new();

        public void CreateCustomerAccount()
        {
            CustomerAccount account = new();

            CreateAccount(account);
        }
        public void CreateAdministratorAccount()
        {
            AdministratorAccount account = new();

            CreateAccount(account);
        }

        private void CreateAccount(IAccount account)
        {
            UIRenderer.ClearArea();
            int yPos = 3;
            UIRenderer.DisplayText("Fyll i informationen nedan.", yPos: yPos);
            account.FirstName = InputManager.RequestNameInput("Förnamn: ", ++yPos);
            account.LastName = InputManager.RequestNameInput("Efternamn: ", ++yPos);

            string userName;

            while (true)
            {
                userName = InputManager.RequestNewUserName(yPos + 1);
                bool isExistingUserName = UserAccounts.Any(user => user.UserName.ToLower() == userName.ToLower());
                if (!isExistingUserName)
                {
                    break;
                }
                else
                {
                    UIRenderer.DisplayInvalidInputMessage("Användarnamnet är redan taget.");
                }
            }

            account.UserName = userName;
            account.MenuItemText = $"{account.LastName}, {account.FirstName}";

            UserAccounts.Add(account);
            UIRenderer.DisplayText("Nytt konto tillagt. Tryck på valfri tangent för att fortsätta", yPos: yPos + 2, maxWidth: 100);
            Console.ReadKey();
        }

        public IAccount LogIn()
        {
            IAccount user;
            UIRenderer.ResetScreen();

            while (true)
            {
                UIRenderer.ClearArea(yPos: 3);
                UIRenderer.DisplayText("Användarnamn: ", yPos: 3);
                string input = Console.ReadLine().ToLower().Trim();

                user = UserAccounts.Where(user => user.UserName.ToLower() == input).FirstOrDefault();

                if (user == null)
                {
                    UIRenderer.DisplayInvalidInputMessage("Det finns ingen användare med det användarnamnet.");
                }
                else
                {
                    break;
                }
            }

            return user;
        }
    }
}
