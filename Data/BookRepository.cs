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
