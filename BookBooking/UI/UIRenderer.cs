using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public static class UIRenderer
    {
        public static void ResetScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Välkommen till Litslenas bibliotek.");
        }

        public static void RenderMenu(List<IMenuItem> menuOptions, int selectedRow)
        {
            Console.SetCursorPosition(0, 2);
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.ForegroundColor = (selectedRow == i) ? ConsoleColor.Green : ConsoleColor.Gray;
                Console.WriteLine($"{i + 1}. {menuOptions[i].MenuItemText}");
            }
        }
    }
}
