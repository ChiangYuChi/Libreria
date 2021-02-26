using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria.Models;
using ECPay.Payment.Integration;



namespace Libreria.Controllers
{
    public class PaymentController : Controller
    {
       
       
        public ActionResult PayBill()
        {
            AllInOne oPayment = new AllInOne();
            oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";
            oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
            oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
            oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
            oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
            oPayment.MerchantID = "2000132";//ECPay提供的特店編號

            return Redirect(oPayment.ServiceURL);

        }


       
        

    }
}