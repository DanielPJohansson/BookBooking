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
        public IMenuItem selectedItem;
        public BaseUI(UserSession session)
        {
            Session = session;
        }

        public virtual void MainMenu()
        {
            List<MenuItem> menuOptions = new();

            AddBaseOptionsToMainMenu(menuOptions);

            MenuItem exitOption = AddExitOption("Logga ut", new MenuItem.MethodToCallOnSelection(LogOut));

            SelectInMenu(menuOptions, exitOption);
        }

        public void AddBaseOptionsToMainMenu(List<MenuItem> menuOptions)
        {
            menuOptions.Add(new MenuItem() { MenuItemText = "Visa bibliotekets boklista", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(ListAllLendables) });
            menuOptions.Add(new MenuItem() { MenuItemText = "Visa dina nuvarande lån", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(ListUsersCurrentLoans) });
        }
        public void ListAllLendables()
        {
            List<IMenuItem> lenadables = new List<IMenuItem>(Session.Library.LendablesInInventory);

            OpenMenuBasedOnList(lenadables, new MenuItem.MethodToCallOnSelection(LendableMenu), new MenuItem.MethodToCallOnSelection(MainMenu));
        }

        private void ListUsersCurrentLoans()
        {
            List<IMenuItem> currentLoans = new();
            currentLoans.AddRange(Session.Library.LendablesInInventory.Where(lendable => lendable.CurrentlyBorrowedBy == Session.User)
                                                .Select(lendable => lendable as IMenuItem)
                                                .ToList());
            OpenMenuBasedOnList(currentLoans, new MenuItem.MethodToCallOnSelection(LendableMenu), new MenuItem.MethodToCallOnSelection(MainMenu));
        }

        public virtual void LendableMenu()
        {
            List<MenuItem> menuOptions = new List<MenuItem>();
            AddBaseOptionsToLendableMenu(menuOptions);
            MenuItem exitOption = AddExitOption("Tillbaka", new MenuItem.MethodToCallOnSelection(ListAllLendables));

            SelectInMenu(menuOptions, exitOption);
        }

        public void AddBaseOptionsToLendableMenu(List<MenuItem> menuOptions)
        {
            if ((selectedItem as ILendable).CurrentlyBorrowedBy == null)
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Låna bok", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(BorrowSelectedLendable) + new MenuItem.MethodToCallOnSelection(ListAllLendables) });
            }
            if ((selectedItem as ILendable).CurrentlyBorrowedBy == Session.User)
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Lämna tillbaka bok", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(ReturnSelectedLendable) + new MenuItem.MethodToCallOnSelection(ListUsersCurrentLoans) });
            }
        }

        public void BorrowSelectedLendable()
        {
            Session.LendableManager.Borrow(selectedItem as ILendable);
        }

        public void ReturnSelectedLendable()
        {
            Session.LendableManager.Return(selectedItem as ILendable);
        }

        public void OpenMenuBasedOnList(List<IMenuItem> itemList, MenuItem.MethodToCallOnSelection methodToCallOnSelection, MenuItem.MethodToCallOnSelection returnToOnExit)
        {
            List<MenuItem> menuOptions = GenerateMenuOptionsFromList(itemList, methodToCallOnSelection);

            MenuItem exitOption = AddExitOption("Tillbaka", returnToOnExit);
            SelectInMenu(menuOptions, exitOption, itemList);
        }

        public void SelectInMenu(List<MenuItem> menuOptions, MenuItem exitOption, List<IMenuItem> itemList = null)
        {
            int selection = MenuNavigator.OpenMenuAndReturnIndexOfSelected(new Menu(menuOptions, exitOption));

            if (itemList != null && selection < itemList.Count)
            {
                selectedItem = itemList[selection];
                UIRenderer.ClearInformationDisplay();
                UIRenderer.DisplayStringList(selectedItem.DisplayInformation(), xPos: 50, yPos: 4);
            }
            else
            {
                UIRenderer.ResetScreen();
            }

            menuOptions[selection].MethodCalledOnSelection();
        }

        public MenuItem AddExitOption(string menuItemText, MenuItem.MethodToCallOnSelection methodToCallOnSelection)
        {
            return new MenuItem() { MenuItemText = menuItemText, MethodCalledOnSelection = methodToCallOnSelection };
        }

        public List<MenuItem> GenerateMenuOptionsFromList(List<IMenuItem> itemList, MenuItem.MethodToCallOnSelection methodToCallOnSelection)
        {
            List<MenuItem> menuOptions = new();
            foreach (IMenuItem item in itemList)
            {
                menuOptions.Add(new MenuItem() { MenuItemText = item.MenuItemText, MethodCalledOnSelection = methodToCallOnSelection });
            }
            return menuOptions;
        }

        public void LogOut()
        {
            Session.SaveData();
            Session.User = null;
            Session.Start();
        }
        public void DefaultMenu()
        {
            List<MenuItem> menuOptions = new List<MenuItem>();

            MenuItem exitOption = AddExitOption("Tillbaka", new MenuItem.MethodToCallOnSelection(MainMenu));

            SelectInMenu(menuOptions, exitOption);
        }
    }
}
