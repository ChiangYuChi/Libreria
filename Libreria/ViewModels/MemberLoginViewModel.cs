using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Libreria.ViewModels
{
    public class MemberLoginViewModel
    {
        [Required]
        [Display(Name = "帳號")]
        public string MemberName { get; set; }
        [Required]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string MemberPassword { get; set; }
    }
}