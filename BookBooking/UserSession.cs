using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class UserSession
    {
        public UserAccount CurrentUser { get; set; }
        public LoanManager LoanManager { get; set; }

        public void Login()
        {
            //UI ui = new UI(this);
            AccountManager accountManager = new();

            UIRenderer.ResetScreen();
            Console.SetCursorPosition(0, 2);
            Console.Write("Användarnamn: ");
            string input = Console.ReadLine();

            CurrentUser = AccountManager.UserAccounts[1];

            if (CurrentUser is AdministratorAccount)
            {
                AdministratorUI ui = new AdministratorUI(this);
                ui.MainMenu();
            }
            else if(CurrentUser is CustomerAccount)
            {
                LoanManager = new LoanManager(CurrentUser as CustomerAccount);
                CustomerUI ui = new CustomerUI(this);
                ui.MainMenu();
            }
        }
    }
}
