using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class ExhibitionVIewModel
    {
        public int ExhibitionID { get; set; }

        /// <summary>
        /// 展覽門票
        /// </summary>
        public decimal ExhibitionPrice { get; set; }

        /// <summary>
        /// 展覽開始時間
        /// </summary>
        public DateTime ExhibitionStartTime { get; set; }

        /// <summary>
        /// 展覽結束時間
        /// </summary>
        public DateTime ExhibitionEndTime { get; set; }

        /// <summary>
        /// 編輯時間
        /// </summary>
        public DateTime EditModifyDate { get; set; }

        /// <summary>
        /// 展覽圖片
        /// </summary>
        public string ExPhoto { get; set; }
        public int ExCustomerId { get; set; }

        /// <summary>
        /// 展覽名稱
        /// </summary>
        public string ExName { get; set; }

        /// <summary>
        /// 展覽簡介
        /// </summary>
        public string ExHibitionIntro { get; set; }

        /// <summary>
        /// 主辦單位
        /// </summary>
        public string MasterUnit { get; set; }

        /// <summary>
        /// 客戶驗證
        /// </summary>
        public bool CustomerVerify { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCanceled { get; set; }
    }
}