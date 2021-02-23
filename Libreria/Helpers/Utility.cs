using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

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

       
    }
}