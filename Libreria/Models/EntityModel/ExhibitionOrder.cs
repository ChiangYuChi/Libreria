namespace Libreria.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExhibitionOrder")]
    public partial class ExhibitionOrder
    {
        [Key]
        public int ExOrderId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int ExCustomerId { get; set; }

        public virtual ExhibitionCustomer ExhibitionCustomer { get; set; }

        public string PaymentState { get; set; }

        public bool customerVerify { get; set; }
    }
}
