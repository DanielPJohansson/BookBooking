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
        public InventoryManager InventoryManager { get; set; }
        public Library Library { get; set; }
        public AccountManager AccountManager { get; set; }

        public UserSession()
        {
            AccountManager = new();
            Library = new();
            InventoryManager = new InventoryManager(this);

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
                AddBaseLibraryContent();
            }
        }

        public void SaveData()
        {
            JsonModel dataToSave = new JsonModel(Library, AccountManager.UserAccounts);
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, TypeNameHandling = TypeNameHandling.Auto };
            string jsonString = JsonConvert.SerializeObject(dataToSave, Formatting.Indented, settings);
            File.WriteAllText("libraryAndAccounts.json", jsonString);
            UIRenderer.DisplayText("Data sparad");
            Console.ReadKey();
        }

        private void AddBaseLibraryContent()
        {
            Library.Name = "Litslenas bibliotek";
            Library.Address = "Litslena-kälsta 6";
            Library.LendablesInInventory = new List<ILendable>()
            {
                new Book("What the dog saw", "Malcom", "Gladwell", 2009),
                new Book("Pol pots leende", "Peter", "Fröberg Idling", 2006),
                new Book("Ängeln i Groznyj", "Åsne", "Seierstad", 2007),
                new Book("Brev från nollpunkten", "Peter", "Englund", 1996),
                new Book("Oryx and crake", "Margaret", "Atwood", 2003),
                new Book("The blind assassin", "Margaret", "Atwood", 2000),
                new Book("The shock doctrine", "Naomi", "Klein", 2007)
            };

            AccountManager.UserAccounts = new List<IAccount>()
            {
                new AdministratorAccount() {FirstName = "Default", LastName = "Admin", UserName = "admin", MenuItemText = "Default admin account" },
                new CustomerAccount() {FirstName = "Lisa", LastName = "Svensson", UserName = "Lisa", MenuItemText = "Lisa Svensson" },
            };
        }

    }
}
