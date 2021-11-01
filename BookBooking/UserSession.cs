using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookBooking
{
    public class UserSession
    {
        public BaseUserAccount User { get; set; }
        public LendableManager LendableManager { get; set; }
        public Library Library { get; set; }
        public AccountManager AccountManager { get; set; }

        public UserSession()
        {
            AccountManager = new();
            Library = new();
            LendableManager = new LendableManager(this);

            Library.LendablesInInventory = new List<ILendable>()
            {
                new Book("What the dog saw", "Malcom", "Gladwell", 544864683546),
                new Book("Pol pots leende", "Peter", "Fröberg Idling", 9789172320741),
                new Book("Ängeln i Groznyj", "Åsne", "Seierstad", 9789100121945),
                new Book("Brev från nollpunkten", "Peter", "Englund", 9174865463),
                new Book("Oryx and crake", "Margaret", "Atwood", 9780770429355),
                new Book("The blind assassin", "Margaret", "Atwood", 9780385720953),
                new Book("The shock doctrine", "Naomi", "Klein", 9780141024530)
            };

            AccountManager.UserAccounts = new List<BaseUserAccount>()
            {
                new AdministratorAccount("Admin", "Admin", "Admin", "Admin"),
                new BaseUserAccount("Edda", "Ranlund", "EddRan", "321dsa"),
            };
            LoadData();
        }

        public void Login()
        {
            do
            {
                UIRenderer.ResetScreen();
                Console.SetCursorPosition(0, 2);
                Console.Write("Användarnamn: ");
                string input = Console.ReadLine().ToLower().Trim();

                User = AccountManager.UserAccounts.Where(user => user.UserName.ToLower() == input).FirstOrDefault();
            }
            while (User == null);


            if (User is AdministratorAccount)
            {
                LogInAsAdministrator();
            }
            else 
            {
                LogInAsCustomer();
            }
        }

        private void LogInAsAdministrator()
        {

            AdministratorUI ui = new AdministratorUI(this);
            ui.MainMenu();
        }

        private void LogInAsCustomer()
        {
            BaseUI ui = new BaseUI(this);
            ui.MainMenu();
        }

        private void LoadData()
        {
            //try
            //{
            //    Library = JsonSerializer.Deserialize<Library>(File.ReadAllText("./library.json"));
            //}
            //catch
            //{
            //    Library = new Library("Litslena", "Litslena-kälsta 6");
            //}
            //try
            //{
            //    AccountManager.UserAccounts = JsonSerializer.Deserialize<List<BaseUserAccount>>(File.ReadAllText("./accounts.json"));
            //}
            //catch
            //{
            //    AccountManager.UserAccounts.Add(new AdministratorAccount("Admin", "Admin", "Admin", "Admin"));
            //}
        }
        public void SaveData()
        {
            string jsonStringLibrary = JsonConvert.SerializeObject(Library, Formatting.Indented);
            //string jsonStringLibrary = JsonSerializer.Serialize(Library);
            Console.WriteLine(jsonStringLibrary);
            string jsonStringAccounts = JsonConvert.SerializeObject(AccountManager.UserAccounts, Formatting.Indented);
            Console.WriteLine(jsonStringAccounts);
            File.WriteAllText("library.json", jsonStringLibrary);
            File.WriteAllText("accounts.json", jsonStringAccounts);
            Console.WriteLine("Data saved");
            Console.ReadKey();
        }

    }
}
