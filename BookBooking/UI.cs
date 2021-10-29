using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class UI
    {
        public void LoginScreen()
        {
            List<MenuItem> menuOptions = new List<MenuItem>() {
                new MenuItem("Logga in som kund", new MenuItem.MethodToCallOnSelection(CustomerMenu)),
                new MenuItem("Logga in som administratör", new MenuItem.MethodToCallOnSelection(AdministratorMenu))};
            UIManager.OpenMenu(menuOptions);
        }

        public void AdministratorMenu()
        {
            List<MenuItem> menuOptions = new List<MenuItem>() { 
                new MenuItem("Sök bok", new MenuItem.MethodToCallOnSelection(DefaultAdministratorMenu)),
                new MenuItem("Visa utlånade böcker", new MenuItem.MethodToCallOnSelection(DefaultAdministratorMenu)),
                new MenuItem("Hantera böcker", new MenuItem.MethodToCallOnSelection(DefaultAdministratorMenu)),
                new MenuItem("Hantera användarkonton", new MenuItem.MethodToCallOnSelection(DefaultAdministratorMenu)),
                new MenuItem("Logga ut", new MenuItem.MethodToCallOnSelection(LoginScreen)) };
            UIManager.OpenMenu(menuOptions);
        }

        public void CustomerMenu()
        {
            List<MenuItem> menuOptions = new List<MenuItem>() {
                new MenuItem("Visa boklista", new MenuItem.MethodToCallOnSelection(ListOfBooks)),
                new MenuItem("Visa kontoinformation", new MenuItem.MethodToCallOnSelection(DefaultCustomerMenu)),
                new MenuItem("Logga ut", new MenuItem.MethodToCallOnSelection(LoginScreen)) };
            UIManager.OpenMenu(menuOptions);
        }

        public void DefaultCustomerMenu()
        {
            List<MenuItem> menuOptions = new List<MenuItem>() { new MenuItem("Gå tillbaka till föregående", new MenuItem.MethodToCallOnSelection(CustomerMenu)) };
            UIManager.OpenMenu(menuOptions);
        }

        public void DefaultAdministratorMenu()
        {
            List<MenuItem> menuOptions = new List<MenuItem>() { new MenuItem("Gå tillbaka till föregående", new MenuItem.MethodToCallOnSelection(AdministratorMenu)) };
            UIManager.OpenMenu(menuOptions);
        }

        public void ListOfBooks()
        {

        }
    }
}
