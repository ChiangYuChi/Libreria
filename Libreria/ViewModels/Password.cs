using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class Password
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "原密碼")]

        public string OriginalPassword { get; set; }
        /// <summary>
        /// 確認密碼專用欄位，不影響資料庫
        /// </summary>
        [NotMapped]
        [Compare("memberPassword")]
        [Display(Name = "確認密碼")]

        public string confirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "新密碼")]

        public string NewPassword { get; set; }
    }
}