using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class Menu
    {
        public List<MenuItem> Content { get; set; }
        public MenuItem OnExitGoTo { get; set; }

        public Menu(List<MenuItem> content, MenuItem onExitGoTo)
        {
            Content = content;
            OnExitGoTo = onExitGoTo;
            Content.Add(OnExitGoTo);
        }
    }
}
