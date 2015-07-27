using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BookClassLibrary
{
    class BinaryFileBookService : IBookService
    {
        private string filePath;
        private ILogger logger;

        public BinaryFileBookService(ILogger logger)
        {
            filePath = "someFile";
            this.logger = logger;
        }

        private void SaveData(BinaryWriter writer, Book book)
        {
            writer.Write(book.Title);
            writer.Write(book.Author);
            writer.Write(book.PublisingHouse);
            writer.Write(book.Genre);
            writer.Write(book.Year);
            writer.Flush();
        }
        private Book LoadData(BinaryReader reader)
        {
            var book = new Book();
            book.Title = reader.ReadString();
            book.Author = reader.ReadString();
            book.PublisingHouse = reader.ReadString();
            book.Genre = reader.ReadString();
            book.Year = reader.ReadInt32();
            return book;
        }

        private void WriteToBinaryFile(Book book, bool append = true)
        {
            append = append && File.Exists(filePath);
            using (Stream stream = File.Open(filePath,  append ? FileMode.Append : FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    SaveData(writer, book);
                }
            }
        }

        private void WriteToBinaryFile(List<Book> books, bool append = true)
        {
            append = append && File.Exists(filePath);
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    foreach (var book in books)
                    {
                        SaveData(writer, book);
                    }
                }
            }
        }

        private List<Book> ReadFromBinaryFile()
        {
            List<Book> books = new List<Book>();
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    while (stream.Position != stream.Length)
                    {
                        books.Add(LoadData(reader));
                    }
                }
            }
            return books;
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                Exception ex = new ArgumentNullException();
                logger.Error(ex.Message);
                throw ex;
            }
            if(ReadFromBinaryFile().Contains(book))
            {
                Exception ex = new Exception("Book is already exists");
                logger.Error(ex.Message);
                throw ex;
            }
            WriteToBinaryFile(book);
        }

        public void RemoveBook(Book removingBook)
        {
            if (removingBook == null)
            {
                Exception ex = new ArgumentNullException();
                logger.Error(ex.Message);
                throw ex;
            }
            if (ReadFromBinaryFile().Contains(removingBook))
            {
                Exception ex = new Exception("Book is not exist");
                logger.Error(ex.Message);
                throw ex;
            }
            List<Book> books = ReadFromBinaryFile();
            int i = books.TakeWhile(book => !book.Equals(removingBook)).Count();
            books.RemoveAt(i);
            WriteToBinaryFile(books, false);
        }


        public List<Book> FindByTitle(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                Exception ex = new ArgumentException("The title is empty");
                logger.Warn(ex.Message);
                throw ex;
            }
            List<Book> books = ReadFromBinaryFile();
            return books.FindAll((book) => book.Title == title);
        }

        public void SortBookByTitle()
        {
            var books = ReadFromBinaryFile();
            books.Sort((b1, b2) => String.Compare(b1.Title, b2.Title, StringComparison.OrdinalIgnoreCase));
            WriteToBinaryFile(books, false); 
        }

        public List<Book> FindByAuthor(string author)
        {
            if (String.IsNullOrEmpty(author))
            {
                Exception ex = new ArgumentException("The author is empty");
                logger.Warn(ex.Message);
                throw ex;
            }
            List<Book> books = ReadFromBinaryFile();
            return books.FindAll((book) => book.Author == author);
        }

        public void SortBookByAuthor()
        {
            var books = ReadFromBinaryFile();
            books.Sort((b1, b2) => String.Compare(b1.Author, b2.Author, StringComparison.OrdinalIgnoreCase));
            WriteToBinaryFile(books, false); 
        }

        public List<Book> FindByPublishingHouse(string ph)
        {
            if (String.IsNullOrEmpty(ph))
            {
                Exception ex = new ArgumentException("The PublishingHouse is empty");
                logger.Warn(ex.Message);
                throw ex;
            }
            List<Book> books = ReadFromBinaryFile();
            return books.FindAll((book) => book.PublisingHouse == ph);
        }

        public void SortBookByPublishingHouse()
        {
            var books = ReadFromBinaryFile();
            books.Sort((Book b1, Book b2) => String.Compare(b1.PublisingHouse, b2.PublisingHouse, StringComparison.OrdinalIgnoreCase));
            WriteToBinaryFile(books, false);
        }

        public List<Book> FindByGenre(string genre)
        {
            if (String.IsNullOrEmpty(genre))
            {
                Exception ex = new ArgumentException("The genre is empty");
                logger.Warn(ex.Message);
                throw ex;
            }
            List<Book> books = ReadFromBinaryFile();
            return books.FindAll((Book book) => book.Genre == genre);
        }

        public void SortBookByGenre()
        {
            var books = ReadFromBinaryFile();
            books.Sort((Book b1, Book b2) => String.Compare(b1.Genre, b2.Genre, true));
            WriteToBinaryFile(books, false);
        }

        public List<Book> FindByYear(int year)
        {
            List<Book> books = ReadFromBinaryFile();
            return books.FindAll((Book book) => book.Year == year);
        }

        public void SortBookByYear()
        {
            var books = ReadFromBinaryFile();
            books.Sort((Book b1, Book b2) => b1.Year.CompareTo(b2.Year));
            WriteToBinaryFile(books, false);
        }
    }
}
