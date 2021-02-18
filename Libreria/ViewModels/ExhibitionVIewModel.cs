using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class ExhibitionVIewModel
    {
        public int ExhibitionID { get; set; }
        public decimal ExhibitionPrice { get; set; }
        public DateTime ExhibitionStartTime { get; set; }
        public DateTime ExhibitionEndTime { get; set; }
        public DateTime EditModifyDate { get; set; }
        public string ExPhoto { get; set; }
        public int ExCustomerId { get; set; }
        public string ExName { get; set; }
    }
}