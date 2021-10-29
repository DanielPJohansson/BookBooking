using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class MenuItem
    {
        public string MenuItemText { get; set; }

        public delegate void MethodToCallOnSelection();

        public MethodToCallOnSelection MethodToCall { get; set; }

        public MenuItem(string menuItemText, MethodToCallOnSelection methodToCall)
        {
            MenuItemText = menuItemText;
            MethodToCall = methodToCall;
        }
    }
}
