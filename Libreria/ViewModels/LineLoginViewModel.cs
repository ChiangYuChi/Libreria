using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class LineLoginViewModel
    {
        [Required]
        public string LineUserID { get; set; }
        /// <summary>
        /// 自動從line token抓取的email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 自動從line token抓取的用戶名
        /// </summary>
        [Required]
        public string diaplayName { get; set; }
    }
}