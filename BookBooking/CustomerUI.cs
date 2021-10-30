using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class CustomerUI : UI
    {
        public CustomerUI(UserSession session) : base(session)
        {
            Session = session;
        }

        public override void MainMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() {MenuItemText = "Visa bibliotekets boklista",    MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllBooks) },
                new MenuItem() {MenuItemText = "Visa nuvarande lån",            MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListUsersCurrentLoans) }
            };

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Logga ut", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(LoginScreen) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }

        private void DefaultCustomerMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>();

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Gå tillbaka till föregående", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(MainMenu) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }

        private void ListUsersCurrentLoans()
        {
            List<IMenuItem> menuOptions = (Session.CurrentUser as CustomerAccount).CurrentLoans.ToList<IMenuItem>();
            SetCalledMethodForAllInList(menuOptions, new IMenuItem.MethodToCallOnSelection(LendableMenu));
            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Tillbaka", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(MainMenu) };

            MenuNavigator.OpenMenuAndReturnSelected(new Menu(menuOptions, exitOption), ref selectedLendableItem);
        }

        public override void LendableMenu()
        {
            ILendable selectedItem = selectedLendableItem as ILendable;

            List<IMenuItem> menuOptions = new List<IMenuItem>();

            if (selectedItem.CurrentlyBorrowedBy == null)
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Låna bok", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(BorrowSelectedItem) + new IMenuItem.MethodToCallOnSelection(ListAllBooks) });
            }
            if (selectedItem.CurrentlyBorrowedBy == Session.CurrentUser)
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Lämna tillbaka bok", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ReturnSelectedItem) + new IMenuItem.MethodToCallOnSelection(ListUsersCurrentLoans) });
            }

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Tillbaka", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllBooks) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));

            void BorrowSelectedItem()
            {
                Session.LoanManager.Borrow(selectedItem);
            }

            void ReturnSelectedItem()
            {
                Session.LoanManager.Return(selectedItem);
            }
        }
    }
}
