using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Author { get; set; }
        public string Supplier { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreateTime { get; set; }
        public string Introduction { get; set; }
        public string MainUrl { get; set; }
        public bool isFav { get; set; }
        public List<string> PreviewUrls { get; set; }
        public int Count { get; set; }
        public int SpecialPrice { get; set; }


    }
}