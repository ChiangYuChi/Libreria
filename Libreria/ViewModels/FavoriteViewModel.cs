using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class FavoriteViewModel
    {
        public int RecordId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Author { get; set; }
        public string Supplier { get; set; }
        public DateTime PublishDate { get; set; } 
    }
}