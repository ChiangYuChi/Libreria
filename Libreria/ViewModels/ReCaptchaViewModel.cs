using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class ReCaptchaViewModel
    {
        public string gRecaptchaResponse { get; set; }
        public string secret { get; set; }
        public string remoteip { get; set; }
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
        public string[] errorCodes {get;set;}
    }
}