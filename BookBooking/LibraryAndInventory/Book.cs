using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBooking
{
    public class Book : ILendable
    {
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public int YearPublished { get; set; }
        public string MenuItemText { get; set; }
        public DateTime DateOfLoan { get; set; }
        public DateTime LastReturnDate { get; set; }
        public IAccount CurrentlyBorrowedBy { get; set; }

        public Book(string title, string authorFirstName, string authorLastName, int yearPublished)
        {
            Title = title;
            AuthorFirstName = authorFirstName;
            AuthorLastName = authorLastName;
            YearPublished = yearPublished;
            MenuItemText = $"{Title}. {AuthorFirstName} {AuthorLastName}";
        }

        public List<string> DisplayInformation()
        {
            List<string> output = new();
            output.Add($"Title: {Title}");
            output.Add($"Författare: {AuthorLastName}, {AuthorFirstName}");
            output.Add($"Utgivningsår: {YearPublished}");

            if (CurrentlyBorrowedBy != null)
            {
                output.Add("För tillfället utlånad");
            }

            return output;
        }
    }
}
