using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class MenuItem : IMenuItem
    {
        public string MenuItemText { get; set; }

        public delegate void MethodToCallOnSelection();
        public MethodToCallOnSelection MethodCalledOnSelection { get; set; }
    }
}
