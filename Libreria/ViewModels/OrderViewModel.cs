using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class OrderViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        public string deliveryMethod { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string recipientName { get; set; }

        /// <summary>
        /// 收件人行動電話
        /// </summary>
        public string recipientCellphone { get; set; }

        /// <summary>
        /// 收件人市內電話
        /// </summary>
        public string recipientTelephone { get; set; }

        /// <summary>
        /// 收件人縣市
        /// </summary>
        public string addressCitySelect { get; set; }

        /// <summary>
        /// 收件人轄區
        /// </summary>
        public string addressRegionSelect { get; set; }

        /// <summary>
        /// 收件人地址
        /// </summary>
        public string recipientAddress {get;set;}

        /// <summary>
        /// 收件人郵遞區號
        /// </summary>
        public string recipientPostalCode { get; set; }

        /// <summary>
        /// 訂購人姓名
        /// </summary>
        public string subscriberName { get; set; }

        /// <summary>
        /// 訂購人行動電話
        /// </summary>
        public string subscriberCellphone { get; set; }

        /// <summary>
        /// 訂購人室內電話
        /// </summary>
        public string subscriberTelephone { get; set; }

        /// <summary>
        /// 訂購人地址
        /// </summary>
        public string subscriberAddress { get; set; }

        /// <summary>
        /// 付款方式
        /// 1為取貨付款 2為ATM 3為信用卡
        /// </summary>
        public int paymentMethod { get; set; }

        /// <summary>
        /// 發票開立方式
        /// 1為二聯式電子發票(存入會員帳號)
        /// 2為二聯式電子發票(手機條碼載具) 
        /// 3為二聯式電子發票(自然人憑證載具)
        /// 4為二聯式電子發票(紙本證明聯)
        /// 5為三聯式電子發票
        /// 6為發票捐贈
        /// </summary>
        public int invoice { get; set; }
    }
}