using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class RentalConfirmViewModel
    {
        /// <summary>
        /// 場地租借開始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 場地租借結束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 展覽開始時間
        /// </summary>
        public DateTime ExhibitionStartTime { get; set; }

        /// <summary>
        /// 展覽結束時間
        /// </summary>
        public DateTime ExhibitionEndTime { get; set; }

        /// <summary>
        /// 租借費用總額
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 客戶姓名
        /// </summary>
        [Required]
        [Display(Name = "姓名")]
        [StringLength(50, ErrorMessage ="最多輸入50個字")]
        //[RegularExpression(@"^[A-Za-z]+$[\D]{2,6}", ErrorMessage = "不能輸入數字")]
        public string ExCustomerName { get; set; }

        /// <summary>
        /// 客戶電話
        /// </summary>
        [Required]
        [Display(Name = "連絡電話")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "請輸入正確電話格式，例:0923463864")]
        public string ExCustomerPhone { get; set; }



        /// <summary>
        /// 客戶Email
        /// </summary>
        [Required]
        [Display(Name = "電子郵件")]
        [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確電子郵件格式，例:abc@gmail.com")]
        public string ExCustomerEmail { get; set; }

        /// <summary>
        /// 展覽簡介
        /// </summary>
        [Required]
        [Display(Name = "展覽簡介")]
        [StringLength(150, ErrorMessage = "最多輸入150個字")]
        public string ExhibitionIntro { get; set; }

        /// <summary>
        /// 主辦單位
        /// </summary>
        [Required]
        [Display(Name = "主辦單位")]
        [StringLength(50, ErrorMessage = "最多輸入50個字")]
        public string MasterUnit { get; set; }


        /// <summary>
        /// 展覽門票
        /// </summary>
        [Required]
        [Display(Name = "票價")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "請輸入數字(不能包含特殊符號)")]
        public string ExhibitionPrice { get; set; }

        /// <summary>
        /// 展覽圖片
        /// </summary>
        [Required]
        public string ExPhoto { get; set; }

        /// <summary>
        /// 展覽名稱
        /// </summary>
        [Required]
        [Display(Name = "展覽名稱")]
        [StringLength(50, ErrorMessage = "最多輸入50個字")]
        public string ExName { get; set; }

        [Required]
        public bool? IsCheck { get; set; }
    }
}