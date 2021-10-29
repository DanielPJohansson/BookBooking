using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class Book
    {
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public int YearPublished { get; set; }
        public int YearPrinted { get; set; }
        public long Isbn { get; set; }

        public Book(string title, string authorFirstName, string authorLastName, long isbn)
        {
            Title = title;
            AuthorFirstName = authorFirstName;
            AuthorLastName = AuthorLastName;
            Isbn = isbn;
        }

        public void MakeReservation()
        {

        }

        public void Lend()
        {

        }
    }
}
