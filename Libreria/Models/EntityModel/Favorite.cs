namespace Libreria.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Favorite")]
    public partial class Favorite
    {
        public int FavoriteId { get; set; }

        public int ProductId { get; set; }

        public int memberId { get; set; }

        public virtual member member { get; set; }

        public virtual Product Product { get; set; }
    }
}
