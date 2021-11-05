using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class AdministratorUI : BaseUI
    {
        public AdministratorUI(UserSession session) : base(session)
        {
        }

        public override void MainMenu()
        {
            List<MenuOption> menuOptions = new();

            AddBaseOptionsToMainMenu(menuOptions);

            menuOptions.Add(new MenuOption() { MenuItemText = "Visa utlånade böcker", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(ListAllCurrentlyBorrowedLendables) });
            menuOptions.Add(new MenuOption() { MenuItemText = "Visa borttagna böcker", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(ListAllRemovedLendables) });
            menuOptions.Add(new MenuOption() { MenuItemText = "Lägg till ny bok", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(AddNewLendable) });
            menuOptions.Add(new MenuOption() { MenuItemText = "Hantera användarkonton", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(ManageAccounts) });

            MenuOption exitOption = AddExitOption("Logga ut", new MenuOption.MethodToCallOnSelection(LogOut));

            UIRenderer.ResetScreen();
            SelectInMenu(menuOptions, exitOption);
        }

        #region Lendable menus
        public void ListAllCurrentlyBorrowedLendables()
        {
            List<IListableAsMenu> currentlyBorrowed = new();
            currentlyBorrowed.AddRange(Session.Library.LendablesInInventory.Where(lendable => lendable.CurrentlyBorrowedBy != null)
                                                .Select(lendable => lendable as IListableAsMenu)
                                                .ToList());
            OpenMenuBasedOnList(currentlyBorrowed, new MenuOption.MethodToCallOnSelection(LendableMenu), new MenuOption.MethodToCallOnSelection(MainMenu));
        }

        public void ListAllRemovedLendables()
        {
            List<IListableAsMenu> removedLendables = new List<IListableAsMenu>(Session.Library.LendablesRemovedFromInventory);

            OpenMenuBasedOnList(removedLendables, new MenuOption.MethodToCallOnSelection(ArchivedLendableMenu), new MenuOption.MethodToCallOnSelection(MainMenu));
        }

        public void AddNewLendable()
        {
            
            Session.Library.LendablesInInventory.Add(Session.InventoryManager.CreateNewBook());
            MainMenu();
        }

        public override void LendableMenu()
        {
            List<MenuOption> menuOptions = new();

            menuOptions.Add(new MenuOption() { MenuItemText = "Hantera bok", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(EditSelectedLendable) });
            AddBaseOptionsToLendableMenu(menuOptions);
            MenuOption exitOption = AddExitOption("Tillbaka", new MenuOption.MethodToCallOnSelection(ListAllLendables));

            SelectInMenu(menuOptions, exitOption);
        }

        public void ArchivedLendableMenu()
        {
            List<MenuOption> menuOptions = new();

            MenuOption exitOption = AddExitOption("Tillbaka", new MenuOption.MethodToCallOnSelection(ListAllRemovedLendables));

            SelectInMenu(menuOptions, exitOption);
        }

        private void EditSelectedLendable()
        {
            List<MenuOption> menuOptions = new List<MenuOption>();

            if ((selectedItem as ILendable).CurrentlyBorrowedBy == null)
            {
                menuOptions.Add(new MenuOption() { MenuItemText = "Ta bort bok", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(RemoveSelectedLenadableFromLibraryInventory) + new MenuOption.MethodToCallOnSelection(ListAllLendables) });
            }
            else
            {
                menuOptions.Add(new MenuOption() { MenuItemText = "Registrera bok som förlorad", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(RemoveSelectedLenadableFromLibraryInventory) + new MenuOption.MethodToCallOnSelection(ListAllLendables) });
                menuOptions.Add(new MenuOption() { MenuItemText = "Registrera bok som återlämnad", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(ReturnSelectedLendable) + new MenuOption.MethodToCallOnSelection(ListAllLendables) });
            }

            MenuOption exitOption = AddExitOption("Tillbaka", new MenuOption.MethodToCallOnSelection(LendableMenu));

            SelectInMenu(menuOptions, exitOption);

            void RemoveSelectedLenadableFromLibraryInventory()
            {
                Session.InventoryManager.RemoveLendable(selectedItem as ILendable);
            }
        }
        #endregion

        #region Account menus
        public void ManageAccounts()
        {
            List<MenuOption> menuOptions = new();

            menuOptions.Add(new MenuOption() { MenuItemText = "Lägg till konto", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(AddNewAccount) });
            menuOptions.Add(new MenuOption() { MenuItemText = "Visa användare", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(ListAllAccounts) });

            MenuOption exitOption = AddExitOption("Tillbaka", new MenuOption.MethodToCallOnSelection(MainMenu));

            UIRenderer.ResetScreen();
            SelectInMenu(menuOptions, exitOption);
        }

        public void AddNewAccount()
        {
            List<MenuOption> menuOptions = new();

            menuOptions.Add(new MenuOption() { MenuItemText = "Lägg till kund", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(AddCustomerAccount) });
            menuOptions.Add(new MenuOption() { MenuItemText = "Lägg till administratör", MethodCalledOnSelection = new MenuOption.MethodToCallOnSelection(AddAdministratorAccount) });

            MenuOption exitOption = AddExitOption("Tillbaka", new MenuOption.MethodToCallOnSelection(ManageAccounts));

            UIRenderer.ResetScreen();
            SelectInMenu(menuOptions, exitOption);
        }

        public void AddCustomerAccount()
        {
            Session.AccountManager.CreateCustomerAccount();
            ManageAccounts();
        }

        public void AddAdministratorAccount()
        {
            Session.AccountManager.CreateAdministratorAccount();
            ManageAccounts();
        }

        public void ListAllAccounts()
        {
            List<IListableAsMenu> accounts = new List<IListableAsMenu>(Session.AccountManager.UserAccounts);

            OpenMenuBasedOnList(accounts, new MenuOption.MethodToCallOnSelection(AccountMenu), new MenuOption.MethodToCallOnSelection(MainMenu));
        }

        public void AccountMenu()
        {
            List<MenuOption> menuOptions = new();

            MenuOption exitOption = AddExitOption("Tillbaka", new MenuOption.MethodToCallOnSelection(ListAllAccounts));

            SelectInMenu(menuOptions, exitOption);
        }
        #endregion

        #region General
        public override void DisplayInformationBasedOnUser()
        {
            List<string> informationToDisplay = new();

            if (selectedItem is ILendable)
            {
                ILendable lendable = selectedItem as ILendable;
                informationToDisplay.AddRange(lendable.InformationAsListOfStrings());

                if (lendable.CurrentlyBorrowedBy == Session.User)
                {
                    informationToDisplay.Add("Lånad av dig.");
                    informationToDisplay.Add($"Lånedatum: {lendable.DateOfLoan:dd/MM/yy}");
                    informationToDisplay.Add($"Återlämnas senast: {lendable.LastReturnDate:dd/MM/yy}");
                }
                else if (lendable.CurrentlyBorrowedBy != null)
                {
                    informationToDisplay.Add($"Lånad av: {lendable.CurrentlyBorrowedBy.FirstName} {lendable.CurrentlyBorrowedBy.LastName}");
                    informationToDisplay.Add($"Lånedatum: {lendable.DateOfLoan:dd/MM/yy}");
                    informationToDisplay.Add($"Återlämnas senast: {lendable.LastReturnDate:dd/MM/yy}");
                }
            }

            if (selectedItem is IAccount)
            {
                informationToDisplay.AddRange(selectedItem.InformationAsListOfStrings());
            }

            UIRenderer.DisplayStringList(informationToDisplay, xPos: 50, yPos: 4);
        }
        #endregion
    }
}
