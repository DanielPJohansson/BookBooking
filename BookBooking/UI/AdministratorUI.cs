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
            List<IMenuItem> menuOptions = AddBaseOptionsToMainMenu();

            menuOptions.Add(new MenuItem() { MenuItemText = "Visa utlånade böcker", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllCurrentlyBorrowedLendables) });
            menuOptions.Add(new MenuItem() { MenuItemText = "Lägg till ny bok", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(DefaultMenu) });
            menuOptions.Add(new MenuItem() { MenuItemText = "Hantera användarkonton", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(DefaultMenu) });

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Logga ut", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(LogOut) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }

        private void DefaultAdministratorMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>();

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Gå tillbaka till föregående", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(MainMenu) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }

        public void ListAllCurrentlyBorrowedLendables()
        {
            List<IMenuItem> menuOptions = new();
            menuOptions.AddRange(Session.Library.LendablesInInventory.Where(lendable => lendable.CurrentlyBorrowedBy != null)
                                                .Select(lendable => lendable as IMenuItem)
                                                .ToList());
            SetCalledMethodForAllInList(menuOptions, new IMenuItem.MethodToCallOnSelection(LendableMenu));

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Tillbaka", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(MainMenu) };

            MenuNavigator.OpenMenuAndReturnSelected(new Menu(menuOptions, exitOption), ref selectedMenuItem);
        }

        public override void LendableMenu()
        {
            ILendable selectedLendable = selectedMenuItem as ILendable;

            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() { MenuItemText = "Hantera bok", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(EditSelectedLendable) }
            };

            AddBaseOptionsToLendableMenu(selectedLendable, menuOptions);

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Tillbaka", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllLendables) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }

        private void EditSelectedLendable()
        {
            ILendable selectedLendable = selectedMenuItem as ILendable;
            List<IMenuItem> menuOptions = new List<IMenuItem>();

            if (selectedLendable.CurrentlyBorrowedBy == null)
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Ta bort bok", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(RemoveSelectedLenadableFromLibraryInventory) + new IMenuItem.MethodToCallOnSelection(ListAllLendables) });
            }
            else
            {
                menuOptions.Add(new MenuItem() { MenuItemText = "Registrera bok som förlorad", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(RemoveSelectedLenadableFromLibraryInventory) + new IMenuItem.MethodToCallOnSelection(ListAllLendables) });
                menuOptions.Add(new MenuItem() { MenuItemText = "Registrera bok som återlämnad", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ReturnSelectedLendable) + new IMenuItem.MethodToCallOnSelection(ListAllLendables) });
            }

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Tillbaka", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllLendables) };
            
            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));

            void RemoveSelectedLenadableFromLibraryInventory()
            {

            }
        }

    }
}
