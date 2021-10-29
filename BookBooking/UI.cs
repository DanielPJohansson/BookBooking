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
            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() {MenuItemText = "Logga in som kund",             MethodToCall = new IMenuItem.MethodToCallOnSelection(CustomerMenu) },
                new MenuItem() {MenuItemText = "Logga in som administratör",    MethodToCall = new IMenuItem.MethodToCallOnSelection(AdministratorMenu) }
            };
            UIManager.OpenMenu(menuOptions);
        }

        public void AdministratorMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() {MenuItemText = "Visa boklista",                 MethodToCall = new IMenuItem.MethodToCallOnSelection(ListAllBooks) },
                new MenuItem() {MenuItemText = "Visa utlånade böcker",          MethodToCall = new IMenuItem.MethodToCallOnSelection(DefaultAdministratorMenu) },
                new MenuItem() {MenuItemText = "Hantera böcker",                MethodToCall = new IMenuItem.MethodToCallOnSelection(DefaultAdministratorMenu) },
                new MenuItem() {MenuItemText = "Hantera användarkonton",        MethodToCall = new IMenuItem.MethodToCallOnSelection(DefaultAdministratorMenu) },
                new MenuItem() {MenuItemText = "Logga ut",                      MethodToCall = new IMenuItem.MethodToCallOnSelection(LoginScreen) }
            };
            UIManager.OpenMenu(menuOptions);
        }

        public void CustomerMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() {MenuItemText = "Visa boklista",                 MethodToCall = new IMenuItem.MethodToCallOnSelection(ListAllBooks) },
                new MenuItem() {MenuItemText = "Visa kontoinformation",         MethodToCall = new IMenuItem.MethodToCallOnSelection(DefaultCustomerMenu) },
                new MenuItem() {MenuItemText = "Logga ut",                      MethodToCall = new IMenuItem.MethodToCallOnSelection(LoginScreen) }
            };
            UIManager.OpenMenu(menuOptions);
        }

        public void DefaultCustomerMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>() 
            { 
                new MenuItem() { MenuItemText = "Gå tillbaka till föregående",  MethodToCall = new IMenuItem.MethodToCallOnSelection(CustomerMenu) } 
            };
            UIManager.OpenMenu(menuOptions);
        }

        public void DefaultAdministratorMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>() 
            { 
                new MenuItem() { MenuItemText = "Gå tillbaka till föregående",  MethodToCall = new IMenuItem.MethodToCallOnSelection(AdministratorMenu) } 
            };
            UIManager.OpenMenu(menuOptions);
        }

        public void ListAllBooks()
        {
            List<IMenuItem> menuOptions = Library.Books.ToList<IMenuItem>();
            UIManager.OpenMenu(menuOptions);
        }
    }
}
