namespace Libreria.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("member")]
    public partial class member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public member()
        {
            Favorites = new HashSet<Favorite>();
            Orders = new HashSet<Order>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int memberId { get; set; }

        [Required]
        [StringLength(50)]
        public string memberName { get; set; }

        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        public string HomeNumber { get; set; }

        public string City { get; set; }

        public string Region { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string memberUserName { get; set; }

        [Required]
        [StringLength(1024)]
        public string memberPassword { get; set; }

        [Column(TypeName = "date")]
        public DateTime birthday { get; set; }

        public int Gender { get; set; }

        [StringLength(10)]
        public string IDnumber { get; set; }

        public int? RoleId { get; set; }

        public string LineUserID { get; set; }

        public bool? Change { get; set; }   

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
