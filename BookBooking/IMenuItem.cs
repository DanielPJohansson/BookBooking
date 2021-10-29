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

        public delegate void MethodToCallOnSelection();

        public MethodToCallOnSelection MethodToCall { get; set; }
    }
}
