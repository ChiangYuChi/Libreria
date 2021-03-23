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

        public int ProductId { get; set; }


        /// <summary>
        /// 本項金額
        /// </summary>
        public decimal DetailPrice { get; set; }

        /// <summary>
        /// 特價
        /// </summary>
        public decimal SpecialPrice { get; set; }
    }
}