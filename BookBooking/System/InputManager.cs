using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookBooking
{
    public static class InputManager
    {
        public static string RequestNameInput(string requestMessage, int yPos = 3)
        {
            bool validName = false;
            string name;

            do
            {
                UIRenderer.ClearArea(yPos: yPos);
                UIRenderer.DisplayText(requestMessage, yPos: yPos);
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    validName = ValidateName(name);
                }
            }
            while (!validName);

            return name;
        }

        public static int RequestYearInput(string requestMessage, int yPos = 3)
        {
            bool validYear = false;
            int year = 0;

            do
            {
                UIRenderer.ClearArea(yPos: yPos);
                UIRenderer.DisplayText(requestMessage, yPos: yPos);
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    validYear = ValidateYear(input, ref year);
                }
            }
            while (!validYear);

            return year;
        }

        public static string RequestNewUserName(int yPos = 3)
        {
            Regex regex = new Regex(@"^[a-zA-ZåäöÅÄÖ0-9]{4,}$");
            string input = "";
            while (true)
            {
                UIRenderer.ClearArea(yPos: yPos);
                UIRenderer.DisplayText("Önskat användarnamn: ", yPos: yPos);
                input = Console.ReadLine();
                
                if(!regex.IsMatch(input))
                {
                    UIRenderer.DisplayInvalidInputMessage("Ange användarnamn med minst fyra tecken bestående av siffror och bokstäver från svenska alfabetet");
                }
                else
                {
                    break;
                }
            }

            return input;
        }

        public static string RequestFreeInput(string requestMessage, int yPos = 3)
        {
            string output;

            while (true)
            {
                UIRenderer.ClearArea(yPos: yPos);
                UIRenderer.DisplayText(requestMessage, yPos: yPos);
                output = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(output))
                {
                    break;
                }
            }

            return output;
        }


        /// <summary>
        /// Accepts a string with names separated by blank spaces
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns></returns>
        public static bool ValidateName(string inputName)
        {
            string[] names = inputName.Trim().Split(' ');
            bool output = true;

            foreach (string name in names)
            {
                if (!char.IsUpper(name[0]))
                {
                    output = false;
                }
                foreach (char letter in name)
                {
                    if (!char.IsLetter(letter))
                    {
                        output = false;
                    }
                }
            }

            if (output == false)
            {
                UIRenderer.DisplayInvalidInputMessage("Namn måste börja med stor bokstav och kan endast innehålla bokstäver.");
            }
            return output;
        }

        public static bool ValidateYear(string input, ref int year)
        {
            bool output = int.TryParse(input, out year);

            if (output == false || year > DateTime.Now.Year || year <= 1436)
            {
                UIRenderer.DisplayInvalidInputMessage("Det kan endast vara ett årtal mellan att tryckpressen uppfanns och innevarande år.");
                return false;
            }

            return output;
        }

    }
}
