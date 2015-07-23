using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BookClassLibrary
{
    interface IBookService
    {
        void AddBook(Book book);
        void RemoveBook(Book book);
        List<Book> FindByTitle(string title);
        void SortBookByTitle();
        List<Book> FindByAuthor(string author);
        void SortBookByAuthor();
        List<Book> FindByPublishingHouse(string ph);
        void SortBookByPublishingHouse();
        List<Book> FindByGenre(string Genre);
        void SortBookByGenre();
        List<Book> FindByYear(int year);
        void SortBookByYear();
    }
}
