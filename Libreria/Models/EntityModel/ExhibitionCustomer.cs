namespace Libreria.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExhibitionCustomer")]
    public partial class ExhibitionCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExhibitionCustomer()
        {
            Exhibitions = new HashSet<Exhibition>();
            ExhibitionOrders = new HashSet<ExhibitionOrder>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExCustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string ExCustomerName { get; set; }

        [Required]
        [StringLength(50)]
        public string ExCustomerPhone { get; set; }

        [Required]
        [StringLength(50)]
        public string ExCustomerEmail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exhibition> Exhibitions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExhibitionOrder> ExhibitionOrders { get; set; }
    }
}
