using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class Menu
    {
        public List<IMenuItem> MenuContent { get; set; }
        public IMenuItem OnExitGoTo { get; set; }

        public Menu(List<IMenuItem> menuContent, IMenuItem onExitGoTo)
        {
            MenuContent = menuContent;
            OnExitGoTo = onExitGoTo;
        }
    }
}
