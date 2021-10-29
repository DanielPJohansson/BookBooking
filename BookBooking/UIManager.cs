using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public static class UIManager
    {

        public static void OpenMenu(List<MenuItem> menuOptions)
        {
            ResetScreen();
            int selection = UIManager.NavigateInMenu(menuOptions);
            menuOptions[selection].MethodToCall();
        }
        private static void ResetScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Välkommen till Litslenas bibliotek.");
        }
        private static int NavigateInMenu(List<MenuItem> menuOptions)
        {
            int selectedRow = 0;
            ConsoleKeyInfo pressedKey;

            do
            {
                RenderMenu(menuOptions, selectedRow);
                pressedKey = Console.ReadKey(true);
                ChangeSelectedRow(menuOptions, ref selectedRow, pressedKey);
            }
            while (pressedKey.Key != ConsoleKey.Enter);

            return selectedRow;
        }
        private static void RenderMenu(List<MenuItem> menuOptions, int selectedRow)
        {
            Console.SetCursorPosition(0, 2);
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.ForegroundColor = (selectedRow == i) ? ConsoleColor.Green : ConsoleColor.White;
                Console.WriteLine($"{i + 1}. {menuOptions[i].MenuItemText}");
            }
        }

        private static int ChangeSelectedRow(List<MenuItem> menuOptions, ref int selectedRow, ConsoleKeyInfo pressedKey)
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
