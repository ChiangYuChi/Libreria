namespace Libreria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeInfo { get; set; }
    }
}
