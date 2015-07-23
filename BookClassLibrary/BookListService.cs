using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClassLibrary
{
    public class BookListService
    {
        private IBookService bookService = new FileBookService("someFile");

        public void AddBook(Book book)
        {
            try
            {
                //book.Title = "TheLandOfMirdad";
                bookService.AddBook(book);
            }
            catch (Exception e)
            {
                Logger.Log.Info("Unhandled exception:");
                Logger.Log.Error(e.StackTrace);
            }
        }

        public void RemoveBook(Book book)
        {
            try
            {
                //book.Title = "TheLandOfMirdad";
                bookService.RemoveBook(book);
            }
            catch (Exception e)
            {
                Logger.Log.Info("Unhandled exception:");
                Logger.Log.Error(e.StackTrace);
            }
        }
    }
}
