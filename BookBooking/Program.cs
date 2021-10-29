using System;
using System.Collections.Generic;

namespace BookBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>()
            {
                new Book("What the dog saw", "Malcom", "Gladwell", 544864683546),
                new Book("Pol pots leende", "Peter", "Fröberg Idling", 9789172320741),
                new Book("Ängeln i Groznyj", "Åsne", "Seierstad", 9789100121945),
                new Book("Brev från nollpunkten", "Peter", "Englund", 9174865463),
            };

            List<UserAccount> users = new List<UserAccount>()
            {
                new UserAccount("Daniel", "Johansson", "DanJoh", "asd123"),
                new UserAccount("Edda", "Ranlund", "EddRan", "321dsa"),
            };

            /*
             * Om ingen inloggning gjorts tidigare skapas ett adminkonto
             * Annars inloggning
             * 
             * Gå till relevant meny
             * 
             * Kund:
             * -Sök bok
             * -Välj bok
             * -Se status för bok
             * -Låna eller reservera
             * 
             * Admin
             * -Lägg till bok
             * -Sök bok
             * -Välj bok
             * -Se status för bok
             * -Ta bort bok ur system
             */

            UI ui = new();
            ui.LoginScreen();

        }
    }
}
