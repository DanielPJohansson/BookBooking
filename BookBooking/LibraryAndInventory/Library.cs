using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookBooking
{
    public class Library
    {
        public List<ILendable> LendablesInInventory { get; set; } = new();

        public List<ILendable> LendablesRemovedFromInventory { get; set; } = new();

        public string Name { get; set; }
        public string Address { get; set; }

        public Library()
        {

        }

        public Library(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
