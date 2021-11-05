using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class Menu
    {
        public List<MenuOption> Content { get; set; }
        public MenuOption OnExitGoTo { get; set; }

        public Menu(List<MenuOption> content, MenuOption onExitGoTo)
        {
            Content = content;
            OnExitGoTo = onExitGoTo;
            Content.Add(OnExitGoTo);
        }
    }
}
