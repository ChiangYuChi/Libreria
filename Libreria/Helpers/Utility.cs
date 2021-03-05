using Libreria.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Libreria.Helpers
{
    public class Utility
    {
    
        //加密
        public static string GetSha512(string Sha512Password)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(Sha512Password);
            byte[] crypto = sha512.ComputeHash(source);
            string result = Convert.ToBase64String(crypto);

            return result;
        }

        public static ReCaptchaViewModel GetRecaptchaVaildation(string gRecaptchaResponse)
        {
            ReCaptchaViewModel reCaptchaVM = new ReCaptchaViewModel
            {
                gRecaptchaResponse = gRecaptchaResponse,
                secret = "6LdHiXIaAAAAAL8QjmqqKhcG0SFueAu8SpDiJ9_p",
                remoteip = "",
            };

            var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("secret", reCaptchaVM.secret),
                    new KeyValuePair<string, string>("response", reCaptchaVM.gRecaptchaResponse),
                }
            );

            HttpClient client = new HttpClient();
            var response = client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            reCaptchaVM = JsonConvert.DeserializeObject<ReCaptchaViewModel>(json);

            return reCaptchaVM;
        }
    }
}