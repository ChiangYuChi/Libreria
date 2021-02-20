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
        /// �o���}�ߤ覡
        /// 1���G�p���q�l�o��(�s�J�|���b��)
        /// 2���G�p���q�l�o��(������X����) 
        /// 3���G�p���q�l�o��(�۵M�H���Ҹ���)
        /// 4���G�p���q�l�o��(�ȥ��ҩ��p)
        /// 5���T�p���q�l�o��
        /// 6���o������
        /// </summary>
        public int InvoiceType { get; set; }

        [StringLength(50)]
        public string InvoiceInfo { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// �I�ڤ覡
        /// 1�����f�I�� 2��ATM 3���H�Υd
        /// </summary>
        public int PaymentType { get; set; }

        public virtual member member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
