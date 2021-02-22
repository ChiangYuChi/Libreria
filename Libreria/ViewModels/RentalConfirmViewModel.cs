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
        /// 
        /// </summary>
        public string StartTime { get; set; }//展覽時間開始
        public string EndTime { get; set; }//展覽時間結束

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
        public string ExCustomerName { get; set; }

        /// <summary>
        /// 客戶電話
        /// </summary>
        public string ExCustomerPhone { get; set; }


        [Required(ErrorMessage ="請輸入Email!")]
        [DataType(DataType.EmailAddress)]
        /// <summary>
        /// 客戶Email
        /// </summary>
        public string ExCustomerEmail { get; set; }

        /// <summary>
        /// 展覽簡介
        /// </summary>
        public string ExhibitionIntro { get; set; }

        /// <summary>
        /// 主辦單位
        /// </summary>
        public string MasterUnit { get; set; }


        /// <summary>
        /// 展覽門票
        /// </summary>
        public decimal ExhibitionPrice { get; set; }

        /// <summary>
        /// 展覽圖片
        /// </summary>
        public string ExPhoto { get; set; }

        /// <summary>
        /// 展覽名稱
        /// </summary>
        public string ExName { get; set; }

    }
}