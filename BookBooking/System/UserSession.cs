using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookBooking
{
    public class UserSession
    {
        public IAccount User { get; set; }
        public LendableManager LendableManager { get; set; }
        public Library Library { get; set; }
        public AccountManager AccountManager { get; set; }

        public UserSession()
        {
            AccountManager = new();
            Library = new();
            LendableManager = new LendableManager(this);

            //AddBaseLibraryContent();
            LoadData();
        }


        public void Start()
        {
            UIRenderer.DisplayText("Välkommen till Litslenas bibliotek.", 0, 0);

            User = AccountManager.LogIn();

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
            try
            {
                var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, TypeNameHandling = TypeNameHandling.Auto };
                JsonModel loadedData = JsonConvert.DeserializeObject<JsonModel>(File.ReadAllText("./libraryAndAccounts.json"), settings);
                Library = loadedData.Library;
                AccountManager.UserAccounts = loadedData.Accounts;
            }
            catch
            {
                Library = new Library("Litslena", "Litslena-kälsta 6");
                AccountManager.UserAccounts.Add(new AdministratorAccount("Admin", "Admin", "Admin", "Admin"));
            }
        }
        public void SaveData()
        {
            JsonModel dataToSave = new JsonModel(Library, AccountManager.UserAccounts);
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, TypeNameHandling = TypeNameHandling.Auto };
            string jsonString = JsonConvert.SerializeObject(dataToSave, Formatting.Indented, settings);
            File.WriteAllText("libraryAndAccounts.json", jsonString);
            Console.WriteLine("Data sparad");
            Console.ReadKey();
        }
        private void AddBaseLibraryContent()
        {
            Library.Name = "Litslenas bibliotek";
            Library.Address = "Litslena-kälsta 6";
            Library.LendablesInInventory = new List<ILendable>()
            {
                new Book("What the dog saw", "Malcom", "Gladwell", 1999),
                new Book("Pol pots leende", "Peter", "Fröberg Idling", 2011),
                new Book("Ängeln i Groznyj", "Åsne", "Seierstad", 1998),
                new Book("Brev från nollpunkten", "Peter", "Englund", 1989),
                new Book("Oryx and crake", "Margaret", "Atwood", 1987),
                new Book("The blind assassin", "Margaret", "Atwood", 1995),
                new Book("The shock doctrine", "Naomi", "Klein", 2008)
            };

            AccountManager.UserAccounts = new List<IAccount>()
            {
                new AdministratorAccount("Admin", "Admin", "Admin", "Admin"),
                new CustomerAccount("Edda", "Ranlund", "EddRan", "321dsa"),
            };
        }

    }
}
