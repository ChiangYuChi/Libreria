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

        /// <summary>
        /// 祇布秨ミよΑ
        /// 1羛Α筿祇布(穦眀腹)
        /// 2羛Α筿祇布(も诀兵絏更ㄣ) 
        /// 3羛Α筿祇布(礛咎靡更ㄣ)
        /// 4羛Α筿祇布(セ靡羛)
        /// 5羛Α筿祇布
        /// 6祇布秘
        /// </summary>
        public int InvoiceType { get; set; }

        [StringLength(50)]
        public string InvoiceInfo { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 蹿よΑ
        /// 1砯蹿 2ATM 3獺ノ
        /// </summary>
        public int PaymentType { get; set; }

        public virtual member member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
