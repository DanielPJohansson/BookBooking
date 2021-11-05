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
        public IListableAsMenu selectedItem;
        public BaseUI(UserSession session)
        {
            Session = session;
        }

        public virtual void MainMenu()
        {
            List<MenuOption> menuOptions = new();

            AddBaseOptionsToMainMenu(menuOptions);

            MenuOption exitOption = AddExitOption("Logga ut", new MenuOption.MethodToCallOnSelection(LogOut));

            UIRenderer.ResetScreen();
            SelectInMenu(menuOptions, exitOption);
        }

        public void AddBaseOptionsToMainMenu(List<MenuOption> menuOptions)
        {
            menuOptions.Add(new MenuOption() { MenuItemText = "Visa bibliotekets boklista", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(ListAllLendables) });
            menuOptions.Add(new MenuOption() { MenuItemText = "Visa dina nuvarande lån", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(ListUsersCurrentLoans) });
        }
        public void ListAllLendables()
        {
            List<IListableAsMenu> lenadables = new List<IListableAsMenu>(Session.Library.LendablesInInventory);

            OpenMenuBasedOnList(lenadables, new MenuOption.MethodToCallOnSelection(LendableMenu), new MenuOption.MethodToCallOnSelection(MainMenu));
        }

        private void ListUsersCurrentLoans()
        {
            List<IListableAsMenu> currentLoans = new();
            currentLoans.AddRange(Session.Library.LendablesInInventory.Where(lendable => lendable.CurrentlyBorrowedBy == Session.User)
                                                .Select(lendable => lendable as IListableAsMenu)
                                                .ToList());
            OpenMenuBasedOnList(currentLoans, new MenuOption.MethodToCallOnSelection(LendableMenu), new MenuOption.MethodToCallOnSelection(MainMenu));
        }

        public virtual void LendableMenu()
        {
            List<MenuOption> menuOptions = new List<MenuOption>();
            AddBaseOptionsToLendableMenu(menuOptions);
            MenuOption exitOption = AddExitOption("Tillbaka", new MenuOption.MethodToCallOnSelection(ListAllLendables));

            SelectInMenu(menuOptions, exitOption);
        }

        public void AddBaseOptionsToLendableMenu(List<MenuOption> menuOptions)
        {
            if ((selectedItem as ILendable).CurrentlyBorrowedBy == null)
            {
                menuOptions.Add(new MenuOption() { MenuItemText = "Låna bok", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(BorrowSelectedLendable) + new MenuOption.MethodToCallOnSelection(ListAllLendables) });
            }
            if ((selectedItem as ILendable).CurrentlyBorrowedBy == Session.User)
            {
                menuOptions.Add(new MenuOption() { MenuItemText = "Lämna tillbaka bok", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(ReturnSelectedLendable) + new MenuOption.MethodToCallOnSelection(ListUsersCurrentLoans) });
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

        public void OpenMenuBasedOnList(List<IListableAsMenu> itemList, MenuOption.MethodToCallOnSelection methodToCallOnSelection, MenuOption.MethodToCallOnSelection returnToOnExit)
        {
            List<MenuOption> menuOptions = GenerateMenuOptionsFromList(itemList, methodToCallOnSelection);

            MenuOption exitOption = AddExitOption("Tillbaka", returnToOnExit);
            UIRenderer.ResetScreen();
            SelectInMenu(menuOptions, exitOption, itemList);
        }

        public void SelectInMenu(List<MenuOption> menuOptions, MenuOption exitOption, List<IListableAsMenu> itemsListedInMenu = null)
        {
            int selection = MenuNavigator.OpenMenuAndReturnIndexOfSelected(new Menu(menuOptions, exitOption));

            if (itemsListedInMenu != null && selection < itemsListedInMenu.Count)
            {
                selectedItem = itemsListedInMenu[selection];
                DisplayInformationBasedOnUser();
            }

            menuOptions[selection].MethodCalledOnSelection();
        }

        public virtual void DisplayInformationBasedOnUser()
        {
            List<string> informationToDisplay = new();

            if(selectedItem is ILendable)
            {
                ILendable lendable = selectedItem as ILendable;
                informationToDisplay.AddRange(lendable.InformationAsListOfStrings());
                
                if(lendable.CurrentlyBorrowedBy == Session.User)
                {
                    informationToDisplay.Add("Lånad av dig.");
                    informationToDisplay.Add($"Återlämnas senast: {lendable.LastReturnDate:dd/MM/yy}");
                }
                else if(lendable.CurrentlyBorrowedBy != null)
                {
                    informationToDisplay.Add("Tillfälligt utlånad.");
                }
            }

            UIRenderer.DisplayStringList(informationToDisplay, xPos: 50, yPos: 4);
        }

        public MenuOption AddExitOption(string menuItemText, MenuOption.MethodToCallOnSelection methodToCallOnSelection)
        {
            return new MenuOption() { MenuItemText = menuItemText, MethodCalledOnSelection = methodToCallOnSelection };
        }

        public List<MenuOption> GenerateMenuOptionsFromList(List<IListableAsMenu> itemList, MenuOption.MethodToCallOnSelection methodToCallOnSelection)
        {
            List<MenuOption> menuOptions = new();
            foreach (IListableAsMenu item in itemList)
            {
                menuOptions.Add(new MenuOption() { MenuItemText = item.MenuItemText, MethodCalledOnSelection = methodToCallOnSelection });
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
            List<MenuOption> menuOptions = new List<MenuOption>();

            MenuOption exitOption = AddExitOption("Tillbaka", new MenuOption.MethodToCallOnSelection(MainMenu));
            
            UIRenderer.ResetScreen();
            SelectInMenu(menuOptions, exitOption);
        }
    }
}
