using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class AdministratorUI : UI
    {
        public AdministratorUI(UserSession session) : base(session)
        {
            Session = session;
        }

        public override void MainMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>()
            {
                new MenuItem() {MenuItemText = "Visa bibliotekets boklista",    MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(ListAllBooks) },
                new MenuItem() {MenuItemText = "Visa utlånade böcker",          MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(DefaultAdministratorMenu) },
                new MenuItem() {MenuItemText = "Hantera böcker",                MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(DefaultAdministratorMenu) },
                new MenuItem() {MenuItemText = "Hantera användarkonton",        MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(DefaultAdministratorMenu) }
            };

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Logga ut", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(LoginScreen) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }

        private void DefaultAdministratorMenu()
        {
            List<IMenuItem> menuOptions = new List<IMenuItem>();

            IMenuItem exitOption =
                new MenuItem() { MenuItemText = "Gå tillbaka till föregående", MethodCalledOnSelection = new IMenuItem.MethodToCallOnSelection(MainMenu) };

            MenuNavigator.OpenMenu(new Menu(menuOptions, exitOption));
        }
    }
}
