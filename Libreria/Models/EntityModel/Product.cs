namespace Libreria.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Favorites = new HashSet<Favorite>();
            OrderDetails = new HashSet<OrderDetail>();
            Previews = new HashSet<Preview>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string ISBN { get; set; }

        public int SupplierId { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        public int Inventory { get; set; }

        public int CategoryId { get; set; }

        [Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }

        public int Sort { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string Introduction { get; set; }

        public int? TotalSales { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Preview> Previews { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
