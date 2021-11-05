using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookBooking
{
    public class CustomerAccount : IAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public string MenuItemText { get; set; }

        public List<string> InformationAsListOfStrings()
        {
            List<string> output = new();
            output.Add($"Namn: {FirstName} {LastName}");
            output.Add("Kund");

            return output;
        }

    }
}
