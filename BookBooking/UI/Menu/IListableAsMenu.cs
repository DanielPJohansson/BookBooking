using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public interface IListableAsMenu
    {
        public string MenuItemText { get; set; }

        public List<string> InformationAsListOfStrings();
    }
}
