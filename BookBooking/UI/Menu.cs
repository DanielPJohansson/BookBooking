using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class Menu
    {
        public List<IMenuItem> Content { get; set; }
        public IMenuItem OnExitGoTo { get; set; }

        public Menu(List<IMenuItem> content, IMenuItem onExitGoTo)
        {
            Content = content;
            OnExitGoTo = onExitGoTo;
            Content.Add(OnExitGoTo);
        }
    }
}
