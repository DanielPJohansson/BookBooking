using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public static class UIManager
    {
        public static void ResetScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Välkommen till Litslenas bibliotek.");
        }
    }
}
