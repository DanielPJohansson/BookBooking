using System;
using System.Collections.Generic;

namespace BookBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            UserSession session = new();
            session.Start();
        }
    }
}
