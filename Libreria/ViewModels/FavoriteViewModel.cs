using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class FavoriteViewModel
    {
        public int Id { get; set; }
        public string Pic { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string SupName { get; set; }
        public DateTime PublishDate { get; set; }
    }
}