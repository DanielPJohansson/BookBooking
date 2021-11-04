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

        public IAccount CreateCustomerAccount()
        {
            UIRenderer.ClearArea();
            int yPos = 3;
            UIRenderer.DisplayText("Fyll i informationen nedan.", yPos: yPos);
            string customerFirstName = InputManager.RequestNameInput("Förnamn: ", ++yPos);
            string customerLastName = InputManager.RequestNameInput("Efternamn: ", ++yPos);

            UIRenderer.DisplayText("Nytt konto tillagt. Tryck på valfri tangent för att fortsätta", yPos: yPos + 2);
            Console.ReadKey();

            return new CustomerAccount("", "", "", "");
        }

        public IAccount CreateAdministratorAccount()
        {
            IAccount administrator = new AdministratorAccount("", "", "", "");
            return administrator;
        }

        public string GenerateUserName()
        {
            throw new NotImplementedException();
        }

        public IAccount LogIn()
        {
            IAccount user;

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
