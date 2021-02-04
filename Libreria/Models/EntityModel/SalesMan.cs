using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Libreria.Models.EntityModel
{
    [Table("SalesMan")]
    public partial class SalesMan
    {
        [Key]
        public int JobNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}