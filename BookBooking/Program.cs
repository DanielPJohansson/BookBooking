using System;
using System.Collections.Generic;

namespace BookBooking
{
    class Program
    {

        static void Main(string[] args)
        {
            Library.Books = new List<Book>()
            {
                new Book("What the dog saw", "Malcom", "Gladwell", 544864683546),
                new Book("Pol pots leende", "Peter", "Fröberg Idling", 9789172320741),
                new Book("Ängeln i Groznyj", "Åsne", "Seierstad", 9789100121945),
                new Book("Brev från nollpunkten", "Peter", "Englund", 9174865463),
                new Book("Oryx and crake", "Margaret", "Atwood", 9780770429355),
                new Book("The blind assassin", "Margaret", "Atwood", 9780385720953),
                new Book("The shock doctrine", "Naomi", "Klein", 9780141024530)
            };

            AccountManager.UserAccounts = new List<UserAccount>()
            {
                new AdministratorAccount("Daniel", "Johansson", "DanJoh", "asd123"),
                new CustomerAccount("Edda", "Ranlund", "EddRan", "321dsa")
            };

            UserSession session = new();

            session.Login();
        }
    }
}
