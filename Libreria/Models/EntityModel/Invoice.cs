namespace Libreria.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        public int InvoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeInfo { get; set; }
    }
}
