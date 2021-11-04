﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    class AdministratorAccount : IAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public AdministratorAccount(string firstName, string lastName, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            IsAdmin = true;
        }

    }
}
