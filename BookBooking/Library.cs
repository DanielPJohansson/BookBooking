using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
   
    public class Library
    {
        public static List<Book> Books { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Library(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
