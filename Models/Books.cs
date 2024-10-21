using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVize.Models
{
    public class Books
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public int PageCount { get; set; }
        public string ISBN { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
