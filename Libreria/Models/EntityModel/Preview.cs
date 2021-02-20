namespace Libreria.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Preview")]
    public partial class Preview
    {
        public int PreviewId { get; set; }

        public int ProductId { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public int Sort { get; set; }

        public virtual Product Product { get; set; }
    }
}
