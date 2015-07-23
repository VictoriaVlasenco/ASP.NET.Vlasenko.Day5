using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BookClassLibrary
{
    class FileBookService : IBookService
    {
        private string filePath;

        public FileBookService(string filePath)
        {
            this.filePath = filePath;
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
                throw new ArgumentNullException();
            if(ReadFromBinaryFile().Contains(book))
                throw new Exception("Book is already exists");
            WriteToBinaryFile(book);
        }

        public void RemoveBook(Book removingBook)
        {
            if (removingBook == null)
                throw new ArgumentNullException();
            if (ReadFromBinaryFile().Contains(removingBook))
                throw new Exception("Book is not exist");
            List<Book> books = ReadFromBinaryFile();
            int i = books.TakeWhile(book => !book.Equals(removingBook)).Count();
            books.RemoveAt(i);
            WriteToBinaryFile(books, false);
        }


        public List<Book> FindByTitle(string title)
        {
            if (String.IsNullOrEmpty(title))
                throw new ArgumentException("The title is empty");
            List<Book> books = ReadFromBinaryFile();
            return books.FindAll((Book book) => book.Title == title);
        }

        public void SortBookByTitle()
        {
            var books = ReadFromBinaryFile();
            books.Sort((Book b1, Book b2) => String.Compare(b1.Title, b2.Title, StringComparison.OrdinalIgnoreCase));
            WriteToBinaryFile(books, false); 
        }

        public List<Book> FindByAuthor(string author)
        {
            if (String.IsNullOrEmpty(author))
                throw new ArgumentException("The title is empty");
            List<Book> books = ReadFromBinaryFile();
            return books.FindAll((Book book) => book.Author == author);
        }

        public void SortBookByAuthor()
        {
            var books = ReadFromBinaryFile();
            books.Sort((Book b1, Book b2) => String.Compare(b1.Author, b2.Author, StringComparison.OrdinalIgnoreCase));
            WriteToBinaryFile(books, false); 
        }

        public List<Book> FindByPublishingHouse(string ph)
        {
            if (String.IsNullOrEmpty(ph))
                throw new ArgumentException("The title is empty");
            List<Book> books = ReadFromBinaryFile();
            return books.FindAll((Book book) => book.PublisingHouse == ph);
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
                throw new ArgumentException("The title is empty");
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
            throw new NotImplementedException();
        }
    }
}
