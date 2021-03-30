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

        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        public string memberName { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        public string HomeNumber { get; set; }

        [StringLength(10)]
        public string City { get; set; }

        [StringLength(10)]
        public string Region { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string memberUserName { get; set; }

        [Required]
        [StringLength(1024)]
        public string memberPassword { get; set; }

        [Column(TypeName = "date")]
        //public DateTime? birthday { get; set; }
        public Nullable<DateTime> birthday { get; set; }

        public int Gender { get; set; }

        [StringLength(10)]
        public string IDnumber { get; set; }

        public int? RoleId { get; set; }

        [StringLength(512)]

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
