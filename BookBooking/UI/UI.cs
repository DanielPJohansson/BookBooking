using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class UI
    {
        public UserSession Session { get; set; }
        public IMenuItem selectedItem;

        public UI(UserSession session)
        {
            Session = session;
        }

        public void LoginScreen()
        {
            UserSession session = new();
            session.Login();
        }

        public void AdministratorMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() {MenuItemText = "Visa bibliotekets boklista",    MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllBooks) },
                new MenuItem() {MenuItemText = "Visa utlånade böcker",          MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(DefaultAdministratorMenu) },
                new MenuItem() {MenuItemText = "Hantera böcker",                MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(DefaultAdministratorMenu) },
                new MenuItem() {MenuItemText = "Hantera användarkonton",        MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(DefaultAdministratorMenu) },
                new MenuItem() {MenuItemText = "Logga ut",                      MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(LoginScreen) }
            };
            MenuNavigator.OpenMenu(menuOptions);
        }

        public void CustomerMenu()
        {
            //Menu menu = new Menu(new List<IMenuItem>()
            //{
            //    new MenuItem() {MenuItemText = "Visa bibliotekets boklista",    MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllBooks) },
            //    new MenuItem() {MenuItemText = "Visa nuvarande lån",            MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListUsersCurrentLoans) },
            //},
            //    new MenuItem() {MenuItemText = "Logga ut",                      MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(LoginScreen) }
            //);

            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() {MenuItemText = "Visa bibliotekets boklista",    MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllBooks) },
                new MenuItem() {MenuItemText = "Visa nuvarande lån",            MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListUsersCurrentLoans) },
                new MenuItem() {MenuItemText = "Logga ut",                      MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(LoginScreen) }
            };
            //MenuNavigator.OpenMenu(menuOptions);
        }

        public void DefaultCustomerMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() { MenuItemText = "Gå tillbaka till föregående",  MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(CustomerMenu) }
            };
            MenuNavigator.OpenMenu(menuOptions);
        }

        public void DefaultAdministratorMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() { MenuItemText = "Gå tillbaka till föregående",  MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(AdministratorMenu) }
            };
            MenuNavigator.OpenMenu(menuOptions);
        }

        public void ListAllBooks()
        {
            List<IMenuItem> menuOptions = Library.Inventory.ToList<IMenuItem>();
            foreach (IMenuItem item in menuOptions)
            {
                item.MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(LendableMenu);
            }
            menuOptions.Add(new MenuItem() { MenuItemText = "Tillbaka", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(CustomerMenu) });
            MenuNavigator.OpenMenuAndReturnSelected(menuOptions, ref selectedItem);
        }

        public void LendableMenu()
        {
            ILendable lendable = selectedItem as ILendable;

            if (lendable.CurrentlyBorrowedBy == null)
            {
                List<IMenuItem> menuOptions = new List<IMenuItem>()
                {
                    new MenuItem() {MenuItemText = "Låna bok",                      MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(MakeLoan) + new IMenuItem.MethodToCallOnSelection(ListAllBooks) },
                    new MenuItem() {MenuItemText = "Tillbaka",                      MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllBooks) }
                };
                MenuNavigator.OpenMenu(menuOptions);
            }
            else
            {
                List<IMenuItem> menuOptions = new List<IMenuItem>()
                {
                    new MenuItem() {MenuItemText = "Tillbaka",                      MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllBooks) }
                };
                MenuNavigator.OpenMenu(menuOptions);
            }
            void MakeLoan()
            {
                Session.LoanManager.Borrow(lendable);
            }
        }

        public void ListUsersCurrentLoans()
        {
            List<IMenuItem> menuOptions = (Session.CurrentUser as CustomerAccount).CurrentLoans.ToList<IMenuItem>();
            foreach (IMenuItem item in menuOptions)
            {
                item.MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(CustomerMenu);
            }
            MenuNavigator.OpenMenuAndReturnSelected(menuOptions, ref selectedItem);
        }
    }
}
