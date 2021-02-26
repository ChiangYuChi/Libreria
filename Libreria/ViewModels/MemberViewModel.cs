using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class MemberViewModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "帳號")]
        public string memberName { get; set; }
        /// <summary>
        /// 手機號碼
        /// </summary>
        [Display(Name = "手機號碼")]
        [Required]
        [StringLength(50)]                                                                          
        public string MobileNumber { get; set; }
        /// <summary>
        /// 市話
        /// </summary>
        [Display(Name = "市話")]

        [StringLength(50)]

        public string HomeNumber { get; set; }

        public string City { get; set; }

        public string Region { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(100)]
        [Display(Name = "地址")]

        public string Address { get; set; }
        /// <summary>
        /// 電子郵件
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "電子郵件")]

        public string Email { get; set; }

        /// <summary>
        /// 用戶名稱
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "姓名")]

        public string memberUserName { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "密碼")]

        public string memberPassword { get; set; }
        /// <summary>
        /// 確認密碼專用欄位，不影響資料庫
        /// </summary>
        [NotMapped]
        [Compare("memberPassword")]
        [Display(Name = "確認密碼")]

        public string confirmPassword { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "生日")]

        public DateTime birthday { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        [Display(Name = "性別")]
        /// 
        public int Gender { get; set; }
        /// <summary>
        /// 身分證字號
        /// </summary>
        [Display(Name = "身分證字號")]

        [Required]
        [StringLength(10)]
        public string IDnumber { get; set; }


    }
}