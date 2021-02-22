using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class OrderDetailViewModel
    {
        /// <summary>
        /// 書名
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public int Quantity { get; set; }
    }
}