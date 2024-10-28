using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UzmLibrary.Models
{
    public class BookDTO
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string AuthorName { get; set; }  // Yazar ismini doğrudan almak için
    public string PublisherName { get; set; } // Yayıncı adını doğrudan almak için
    public string CategoryName { get; set; }  // Kategori adını doğrudan almak için
    public int PublicationYear { get; set; }
    public int PageCount { get; set; }
    public string ISBN { get; set; }
    public string Language { get; set; }
    public int Stock { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
}

}