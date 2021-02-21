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
        public string memberName { get; set; }
        /// <summary>
        /// 手機號碼
        /// </summary>
        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }
        /// <summary>
        /// 市話
        /// </summary>
        [StringLength(50)]
        public string HomeNumber { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        /// <summary>
        /// 電子郵件
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 用戶名稱
        /// </summary>
        [Required]
        [StringLength(50)]
        public string memberUserName { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [StringLength(50)]
        public string memberPassword { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime birthday { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 身分證字號
        /// </summary>
        [Required]
        [StringLength(10)]
        public string IDnumber { get; set; }


    }
}