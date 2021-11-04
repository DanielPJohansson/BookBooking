using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class JsonModel
    {
        public Library Library { get; set; }
        public List<IAccount> Accounts { get; set; }

        public JsonModel(Library library, List<IAccount> accounts)
        {
            Library = library;
            Accounts = accounts;
        }
    }
}
