using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class ExhibitionVIewModel
    {
        public string Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string masterUnit { get; set; }
        public string performer { get; set; }
        public string img { get; set; }


    }
}