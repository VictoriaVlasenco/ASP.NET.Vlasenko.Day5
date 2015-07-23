using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClassLibrary;

namespace BookConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book();
            book.Title = "TheLandOfMirdad";
            BookListService bls = new BookListService();
            bls.AddBook(book);
            book.Title = "asdf";
            bls.RemoveBook(book);
            Console.Read();
        }
    }
}
