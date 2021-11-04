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
            ClearArea(0, 1, 200, 20);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void RenderMenu(List<MenuItem> menuOptions, int selectedRow, int xPos = 2, int yPos = 4)
        {
            for (int i = 0; i < menuOptions.Count; i++)
            {
                //Console.SetCursorPosition(xPos, yPos + i);
                ConsoleColor color = (selectedRow == i) ? ConsoleColor.Green : ConsoleColor.Gray;
                DisplayText($"{i + 1}. {menuOptions[i].MenuItemText}", xPos, yPos + i, color: color);
            }
        }

        public static void DisplayInvalidInputMessage(string message, int xPos = 0, int yPos = 2)
        {
            Console.SetCursorPosition(xPos, yPos);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
        }

        public static void DisplayText(string text, int xPos = 2, int yPos = 2, int maxWidth = 50, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            string output = CropStringToLength(text, maxWidth);
            Console.SetCursorPosition(xPos, yPos);
            Console.Write(output);
        }

        private static string CropStringToLength(string text, int maxWidth = 50)
        {
            return text.Length <= maxWidth ? text : text.Substring(0, maxWidth);
        }

        public static void DisplayStringList(List<string> text, int xPos = 2, int yPos = 2, int maxWidth = 50)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < text.Count; i++)
            {
                DisplayText(text[i], xPos, yPos + i);
            }
        }

        public static void ClearArea(int xPos = 0, int yPos = 1, int width = 100, int rows = 20)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                Console.SetCursorPosition(xPos, yPos + i);
                stringBuilder = new StringBuilder().Append(' ', width);
                Console.WriteLine(stringBuilder.ToString());
            }
        }

        public static void ClearInformationDisplay()
        {
            ClearArea(xPos: 50, yPos: 3, width: 50);
        }
    }
}
