using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVize.Models;

namespace WebVize.Data
{
    public class BookRepository
    {
        private static List<Books> _books = null;

        static BookRepository()
        {
            _books = new List<Books>
            {
                new Books()
                {
                    Title = "The Catcher in the Rye", 
                    Author = "J.D. Salinger", 
                    Publisher = "Little, Brown and Company", 
                    PublicationYear = 1951, 
                    PageCount = 277, 
                    Language = "English", 
                    Category = "Fiction", 
                    ISBN = "9780316769488", 
                    Stock = 5, 
                    ImageUrl = "/img/catcher.jpg", 
                    Description = "A story about teenage rebellion and angst."
                },
                new Books()
                {
                    Title = "To Kill a Mockingbird", 
                    Author = "Harper Lee", 
                    Publisher = "J.B. Lippincott & Co.", 
                    PublicationYear = 1960, 
                    PageCount = 281, 
                    Language = "English", 
                    Category = "Fiction", 
                    ISBN = "9780060935467", 
                    Stock = 3, 
                    ImageUrl = "/img/mockingbird.jpg", 
                    Description = "A novel about the serious issues of rape and racial inequality."
                },
                new Books()
                {
                    Title = "1984", 
                    Author = "George Orwell", 
                    Publisher = "Secker & Warburg", 
                    PublicationYear = 1949, 
                    PageCount = 328, 
                    Language = "English",
                    Category = "Dystopian", 
                    ISBN = "9780451524935", 
                    Stock = 7, 
                    ImageUrl = "/img/1984.jpg", 
                    Description = "A dystopian novel set in a totalitarian society."
                }
            };
        }

        public static List<Books> Books
        {
            get { return _books; }
        }

        public static void AddBook(Books book)
        {
            _books.Add(book);
        }

        public static Books GetBookByISBN(string isbn)
        {
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }
    }
}
