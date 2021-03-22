using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Libreria.Models.EntityModel 
{
    [Table("Manager")]
    public class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagerId { get; set; }

        [Required]
        [StringLength(50)]
        public string ManagerName { get; set; }

        [Required]
        [StringLength(50)]
        public string ManagerPassword { get; set; }

        [StringLength(50)]
        public string ManagerUsername { get; set; }

        public string ManagerPhoto { get; set; }

        public int ManagerRoleId { get; set; }
    }
}