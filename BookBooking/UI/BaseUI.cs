using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class BaseUI
    {
        public UserSession Session { get; set; }
        public IMenuItem selectedMenuItem;
        public BaseUI(UserSession session)
        {
            Session = session;
        }

        public virtual void MainMenu()
        {
            List<IMenuItem> menuOptions = AddBaseOptionsToMainMenu();

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Logga ut", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(LogOut) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }

        public List<IMenuItem> AddBaseOptionsToMainMenu()
        {
            return new List<IMenuItem>()
            {
                new MenuItem() {MenuItemText = "Visa bibliotekets boklista", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllLendables) },
                new MenuItem() {MenuItemText = "Visa dina nuvarande lån", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListUsersCurrentLoans) }
            };
        }

        public void DefaultMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>();

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Gå tillbaka till föregående", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(MainMenu) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }

        private void ListUsersCurrentLoans()
        {
            List<IMenuItem> menuOptions = Session.User.CurrentLoans.ToList<IMenuItem>();
            SetCalledMethodForAllInList(menuOptions, new IMenuItem.MethodToCallOnSelection(LendableMenu));
            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Tillbaka", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(MainMenu) };

            MenuNavigator.OpenMenuAndReturnSelected(new Menu(menuOptions, exitOption), ref selectedMenuItem);
        }

        public virtual void LendableMenu()
        {
            ILendable selectedLendable = selectedMenuItem as ILendable;

            List<IMenuItem> menuOptions = new List<IMenuItem>();
            AddBaseOptionsToLendableMenu(selectedLendable, menuOptions);

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Tillbaka", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllLendables) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }

        public void AddBaseOptionsToLendableMenu(ILendable selectedLendable, List<IMenuItem> menuOptions)
        {
            if (selectedLendable.CurrentlyBorrowedBy == null)
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Låna bok", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(BorrowSelectedItem) + new IMenuItem.MethodToCallOnSelection(ListAllLendables) });
            }
            if (selectedLendable.CurrentlyBorrowedBy == Session.User)
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Lämna tillbaka bok", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ReturnSelectedLendable) + new IMenuItem.MethodToCallOnSelection(ListUsersCurrentLoans) });
            }
        }

        public  void BorrowSelectedItem()
        {
            Session.LendableManager.Borrow(selectedMenuItem as ILendable);
        }

        public void ReturnSelectedLendable()
        {
            Session.LendableManager.Return(selectedMenuItem as ILendable);
        }

        public void ListAllLendables()
        {
            List<IMenuItem> menuOptions = Session.Library.LendablesInInventory.ToList<IMenuItem>();
            //TODO testa lägga in loop och sakpa nya menuitems istället för att implementera IMenuItem på Lendables.

            SetCalledMethodForAllInList(menuOptions, new IMenuItem.MethodToCallOnSelection(LendableMenu));

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Tillbaka", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(MainMenu) };

            MenuNavigator.OpenMenuAndReturnSelected(new Menu(menuOptions, exitOption), ref selectedMenuItem);
        }

        public void SetCalledMethodForAllInList(List<IMenuItem> menuOptions, IMenuItem.MethodToCallOnSelection method)
        {
            foreach (IMenuItem item in menuOptions)
            {
                item.MethodCalledOnSelection = method;
            }
        }

        public void LogOut()
        {
            Session.SaveData();
            Session.User = null;
            Session.Login();
        }
    }
}
