using Libreria.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class FavoriteViewModel
    {
        public int RecordId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string FavoriteId { get; set; }

        public int ProductId { get; set; }

        public int memberId { get; set; }
        public string Author { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}