using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public static class MenuNavigator
    {
        public static int OpenMenuAndReturnIndexOfSelected(Menu menu)
        {
            UIRenderer.ClearArea(yPos: 3, width: 50);
            int selected = SelectInMenu(menu.Content);
            return selected;
        }

        private static int SelectInMenu(List<MenuOption> menuOptions)
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

        private static int ChangeSelectedRow(List<MenuOption> menuOptions, ref int selectedRow, ConsoleKeyInfo pressedKey)
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
