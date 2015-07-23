using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BookClassLibrary
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublisingHouse { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        

        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;
            if (Author != other.Author || Title != other.Title || PublisingHouse != other.PublisingHouse ||
                Genre != other.Genre || Year != other.Year)
                return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            Book book;
            if (ReferenceEquals(obj, null))
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            if ( (book = obj as Book) == null)
                return false;
            return Equals(book);
        }

        public int CompareTo(Book other)
        {
            if (ReferenceEquals(other, this))
                return 0;
            if (ReferenceEquals(other, null))
                return 1;
            int res = String.Compare(Title, other.Title, true);
            if (res == 0)
            {
                if ((res = String.Compare(Author, other.Author, true)) == 0)
                {
                    if ((res = String.Compare(PublisingHouse, other.PublisingHouse, true)) == 0)
                    {
                        if ((res = String.Compare(Genre, other.Genre, true)) == 0)
                        {
                            res = Year.CompareTo(other.Year);
                        }
                    }
                }
            }
            return res;
        }

        public override int GetHashCode()
        {
            int res;
            unchecked
            {
                res = Title.GetHashCode() + Author.GetHashCode() + PublisingHouse.GetHashCode() +
                      Genre.GetHashCode() + Year;
            }
            return res;
        }
    }
}
