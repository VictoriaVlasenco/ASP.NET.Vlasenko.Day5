using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClassLibrary
{
    public class BookListService
    {
        private IBookService bookService = new BinaryFileBookService(new NLoggerAdapter());

        public void AddBook(Book book)
        {
            try
            {
                bookService.AddBook(book);
            }
            catch (Exception e)
            {
                NLoggerAdapter.Log.Info("Unhandled exception:");
                NLoggerAdapter.Log.Error(e.StackTrace);
            }
        }

        public void RemoveBook(Book book)
        {
            try
            {
                bookService.RemoveBook(book);
            }
            catch (Exception e)
            {
                
            }
        }

        List<Book> FindByTitle(string title)
        {
            
        }

        void SortBookByTitle()
        {
            
        }

        List<Book> FindByAuthor(string author)
        {
            
        }

        void SortBookByAuthor()
        {
            
        }

        List<Book> FindByPublishingHouse(string ph)
        {
            
        }

        void SortBookByPublishingHouse()
        {
            
        }

        List<Book> FindByGenre(string genre)
        {
            
        }

        void SortBookByGenre()
        {
            
        }

        List<Book> FindByYear(int year)
        {
            
        }

        void SortBookByYear()
        {
            
        }
    }
}
