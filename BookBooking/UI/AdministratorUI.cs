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
            List<MenuItem> menuOptions = new();

            AddBaseOptionsToMainMenu(menuOptions);

            menuOptions.Add(new MenuItem() { MenuItemText = "Visa utlånade böcker", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(ListAllCurrentlyBorrowedLendables) });
            menuOptions.Add(new MenuItem() { MenuItemText = "Visa borttagna böcker", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(ListAllRemovedLendables) });
            menuOptions.Add(new MenuItem() { MenuItemText = "Lägg till ny bok", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(AddNewLendable) });
            menuOptions.Add(new MenuItem() { MenuItemText = "Hantera användarkonton", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(DefaultMenu) });

            MenuItem exitOption = AddExitOption("Logga ut", new MenuItem.MethodToCallOnSelection(LogOut));

            SelectInMenu(menuOptions, exitOption);
        }

        public void ListAllCurrentlyBorrowedLendables()
        {
            List<IMenuItem> currentlyBorrowed = new();
            currentlyBorrowed.AddRange(Session.Library.LendablesInInventory.Where(lendable => lendable.CurrentlyBorrowedBy != null)
                                                .Select(lendable => lendable as IMenuItem)
                                                .ToList());
            OpenMenuBasedOnList(currentlyBorrowed, new MenuItem.MethodToCallOnSelection(LendableMenu), new MenuItem.MethodToCallOnSelection(MainMenu));
        }

        public void ListAllRemovedLendables()
        {
            List<IMenuItem> removedLendables = new List<IMenuItem>(Session.Library.LendablesRemovedFromInventory);

            OpenMenuBasedOnList(removedLendables, new MenuItem.MethodToCallOnSelection(ArchivedLendableMenu), new MenuItem.MethodToCallOnSelection(MainMenu));
        }

        public void AddNewLendable()
        {
            
            Session.Library.LendablesInInventory.Add(Session.LendableManager.CreateNewBook());
            MainMenu();
        }

        public override void LendableMenu()
        {
            List<MenuItem> menuOptions = new();

            menuOptions.Add(new MenuItem() { MenuItemText = "Hantera bok", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(EditSelectedLendable) });
            AddBaseOptionsToLendableMenu(menuOptions);
            MenuItem exitOption = AddExitOption("Tillbaka", new MenuItem.MethodToCallOnSelection(ListAllLendables));

            SelectInMenu(menuOptions, exitOption);
        }

        public void ArchivedLendableMenu()
        {
            List<MenuItem> menuOptions = new();

            MenuItem exitOption = AddExitOption("Tillbaka", new MenuItem.MethodToCallOnSelection(ListAllRemovedLendables));

            SelectInMenu(menuOptions, exitOption);
        }

        private void EditSelectedLendable()
        {
            List<MenuItem> menuOptions = new List<MenuItem>();

            if ((selectedItem as ILendable).CurrentlyBorrowedBy == null)
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Ta bort bok", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(RemoveSelectedLenadableFromLibraryInventory) + new MenuItem.MethodToCallOnSelection(ListAllLendables) });
            }
            else
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Registrera bok som förlorad", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(RemoveSelectedLenadableFromLibraryInventory) + new MenuItem.MethodToCallOnSelection(ListAllLendables) });
                menuOptions.Add(new MenuItem() { MenuItemText = "Registrera bok som återlämnad", MethodCalledOnSelection = new MenuItem.MethodToCallOnSelection(ReturnSelectedLendable) + new MenuItem.MethodToCallOnSelection(ListAllLendables) });
            }

            MenuItem exitOption = AddExitOption("Tillbaka", new MenuItem.MethodToCallOnSelection(ListAllLendables));

            SelectInMenu(menuOptions, exitOption);

            void RemoveSelectedLenadableFromLibraryInventory()
            {
                Session.LendableManager.RemoveLendable(selectedItem as ILendable);
            }
        }

    }
}
