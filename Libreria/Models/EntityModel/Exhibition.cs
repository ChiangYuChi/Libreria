namespace Libreria.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Exhibition")]
    public partial class Exhibition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExhibitionID { get; set; }

        public DateTime ExhibitionStartTime { get; set; }

        public DateTime ExhibitionEndTime { get; set; }

        [Required]
        public string ExhibitionIntro { get; set; }

        [Required]
        [StringLength(50)]
        public string MasterUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal ExhibitionPrice { get; set; }

        public DateTime EditModifyDate { get; set; }

        public int ExCustomerId { get; set; }

        [Required]
        public string ExPhoto { get; set; }

        public virtual ExhibitionCustomer ExhibitionCustomer { get; set; }
    }
}
