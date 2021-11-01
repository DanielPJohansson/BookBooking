using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public static class MenuNavigator
    {
        public static void OpenMenu(Menu menu)
        {
            UIRenderer.ResetScreen();
            int selected = SelectInMenu(menu.Content);
            menu.Content[selected].MethodCalledOnSelection();
        }
        public static void OpenMenuAndReturnSelected(Menu menu, ref IMenuItem selectedItem)
        {
            UIRenderer.ResetScreen();
            int selected = SelectInMenu(menu.Content);
            selectedItem = menu.Content[selected];
            menu.Content[selected].MethodCalledOnSelection();
        }

        private static int SelectInMenu(List<IMenuItem> menuOptions)
        {
            int selectedRow = 0;
            ConsoleKeyInfo pressedKey;

            do
            {
                UIRenderer.RenderMenu(menuOptions, selectedRow);
                pressedKey = Console.ReadKey(true);
                ChangeSelectedRow(menuOptions, ref selectedRow, pressedKey);
            }
            while (pressedKey.Key != ConsoleKey.Enter);

            return selectedRow;
        }

        private static int ChangeSelectedRow(List<IMenuItem> menuOptions, ref int selectedRow, ConsoleKeyInfo pressedKey)
        {
            if (pressedKey.Key == ConsoleKey.UpArrow && selectedRow > 0)
            {
                selectedRow--;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow && selectedRow < menuOptions.Count - 1)
            {
                selectedRow++;
            }

            return selectedRow;
        }
    }
}
