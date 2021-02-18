namespace Libreria.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        public DateTime ShippingDate { get; set; }

        public DateTime OrderDate { get; set; }

        public int memberId { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipName { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipCity { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipRegion { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipPostalCode { get; set; }

        public int InvoiceType { get; set; }

        [StringLength(50)]
        public string InvoiceInfo { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int PaymentType { get; set; }

        public virtual member member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
