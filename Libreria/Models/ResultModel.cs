using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Models
{
    public class ResultModel
    {
        public bool isException { get; set; }
        public string ExceptionMsg { get; set; }
        public object data { get; set; }
    }
}