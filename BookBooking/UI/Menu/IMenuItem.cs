using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public interface IMenuItem
    {
        public string MenuItemText { get; set; }

        public List<string> DisplayInformation();
        //public delegate void MethodToCallOnSelection();
        //public MethodToCallOnSelection MethodCalledOnSelection { get; set; }

        //TODO add a display information method. Can be used to display book information
    }
}
