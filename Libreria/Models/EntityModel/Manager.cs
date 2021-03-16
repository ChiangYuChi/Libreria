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
        public string ManagerId { get; set; }

        [StringLength(50)]
        public string ManagerUsername { get; set; }

        [StringLength(50)]
        public string ManagerPassword { get; set; }
        public string ManagerPhoto { get; set; }

        [StringLength(50)]
        public string ManagerName { get; set; }

        [StringLength(50)]
        public string JobTitle { get; set; }
    }
}