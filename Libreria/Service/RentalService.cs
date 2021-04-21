using ECPay.Payment.Integration;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HttpMethod = ECPay.Payment.Integration.HttpMethod;

namespace Libreria.Service
{
    public class RentalService
    {
        private readonly LibreriaRepository _DbRepository;

        public RentalService()
        {
            _DbRepository = new LibreriaRepository();
        }
        
        public async  Task<OperationResult>  ConfirmBooling(RentalConfirmViewModel model)
        {
            var result = new OperationResult();

            LibreriaDataModel context = new LibreriaDataModel();

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    //圖片上傳至imgur並取得圖片網址
                   string imageUrl = null;
                    if (model.ExPhoto.ContentLength > 0)
                    {
                        model.ExPhoto.InputStream.Position = 0;
                        await model.ExPhoto.InputStream.FlushAsync();
                        var apiClient = new ApiClient("8b8585e4ec973fc");
                        var httpClient = new HttpClient();
                        var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
                        var imageUpload = await imageEndpoint.UploadImageAsync(model.ExPhoto.InputStream);
                        imageUrl = imageUpload.Link;
                    }

                    ExhibitionCustomer exhibitionCustomer = new ExhibitionCustomer()
                    {
                        ExCustomerName = model.ExCustomerName,
                        ExCustomerPhone = model.ExCustomerPhone,
                        ExCustomerEmail = model.ExCustomerEmail
                    };
                    _DbRepository.Create(exhibitionCustomer);
                    ExhibitionOrder exhibitionOrder = new ExhibitionOrder()
                    {
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        Price = ((model.EndDate - model.StartDate).Days + 1) * 1500,
                        ExCustomerId = exhibitionCustomer.ExCustomerId,
                    };
                    _DbRepository.Create(exhibitionOrder);

                    model.ExOrderId = exhibitionOrder.ExOrderId;



                    Exhibition entity = new Exhibition()
                    {
                        ExhibitionStartTime = model.ExhibitionStartTime,
                        ExhibitionEndTime = model.ExhibitionEndTime,
                        ExhibitionIntro = model.ExhibitionIntro,
                        MasterUnit = model.MasterUnit,
                        ExhibitionPrice = Convert.ToDecimal(model.ExhibitionPrice),
                        EditModifyDate = DateTime.Now,
                        ExCustomerId = exhibitionCustomer.ExCustomerId,
                        ExPhoto = imageUrl,
                        ExName = model.ExName
                    };
                    _DbRepository.Create(entity);

                    result.IsSuccessful = true;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    result.IsSuccessful = false;
                    result.exception = ex;
                    transaction.Rollback();
                }
            }

            return result;
        }

        //取得目前有預約的日期
        public IEnumerable<string> GetPickDateRange()
        {
            var exhibitionOrders = _DbRepository.GetAll<ExhibitionOrder>().ToList();
            var convertDateRange = exhibitionOrders.Select(x =>
             {
                 var days = (x.EndDate - x.StartDate).Days + 1;
                 return Enumerable.Range(0, days).Select(index => x.StartDate.AddDays(index).ToString("yyyy/MM/dd"));
             });
            var listDateRange = convertDateRange.SelectMany(s => s);

            return listDateRange.Distinct();
        }


        public string ECPay(RentalConfirmViewModel rentalVM)
        {
            List<string> enErrors = new List<string>();
            string html = string.Empty;
            
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    
                    var orderTotal = ((rentalVM.EndDate - rentalVM.StartDate).Days + 1) * 1500;

                    /* 服務參數 */
                    oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
                    oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
                    oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
                    oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
                    oPayment.MerchantID = "2000132";//ECPay提供的特店編號

                    /* 基本參數 */
                    oPayment.Send.ReturnURL = "https://weblibreria.azurewebsites.net//Rental/ConfirmBooling";//付款完成通知回傳的網址
                    oPayment.Send.ClientBackURL = "http://127.0.0.1:4040";//瀏覽器端返回的廠商網址
                    oPayment.Send.OrderResultURL = $"https://weblibreria.azurewebsites.net/Rental/PayReturnDetail?orderId={rentalVM.ExOrderId}";//瀏覽器端回傳付款結果網址
                    oPayment.Send.MerchantTradeNo = "n" + "ECPay" + new Random().Next(0, 99999).ToString();//廠商的交易編號
                    oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間
                    /*oPayment.Send.TotalAmount = Decimal.Parse($"{ orderVM.OrderPrice}") */
                    ;//交易總金額
                    oPayment.Send.TotalAmount = Decimal.Parse($"{String.Format("{0:N0}", orderTotal)}".ToString());
                    oPayment.Send.TradeDesc = "交易描述";//交易描述
                    oPayment.Send.ChoosePayment = PaymentMethod.ALL;//使用的付款方式
                    oPayment.Send.Remark = "";//備註欄位
                    oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
                    oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.No;//是否需要額外的付款資訊
                    oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
                    oPayment.Send.IgnorePayment = ""; //不顯示的付款方式
                    oPayment.Send.PlatformID = "";//特約合作平台商代號
                    oPayment.Send.HoldTradeAMT = HoldTradeType.Yes;
                    oPayment.Send.CustomField1 = "";
                    oPayment.Send.CustomField2 = "";
                    oPayment.Send.CustomField3 = "";
                    oPayment.Send.CustomField4 = "";
                    oPayment.Send.EncryptType = 1;

                    //var orderDetail = orderVM.OrderDetailList.FirstOrDefault();
                    //oPayment.Send.Items.Add(new Item()
                    //{
                    //    Name = orderDetail.ProductName,//商品名稱
                    //    Price = Decimal.Parse($"{orderDetail.UnitPrice}".ToString())   ,//商品單價
                    //    Currency = "新台幣",//幣別單位
                    //    Quantity = orderDetail.Quantity,//購買數量
                    //    URL = "http://google.com",//商品的說明網址

                    //});

                    
                        oPayment.Send.Items.Add(new Item()
                        {
                            Name = "場地租借",//商品名稱
                            Price = Decimal.Parse($"{orderTotal}".ToString()),//商品單價
                            Currency = "新台幣",//幣別單位
                            Quantity = Int32.Parse("1"),//購買數量
                            URL = "http://google.com",//商品的說明網址

                        });
                    


                    //訂單的商品資料

                    /*************************非即時性付款:ATM、CVS 額外功能參數**************/

                    #region ATM 額外功能參數

                    //oPayment.SendExtend.ExpireDate = 3;//允許繳費的有效天數
                    //oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
                    //oPayment.SendExtend.ClientRedirectURL = "";//Client 端回傳付款相關資訊

                    #endregion


                    #region CVS 額外功能參數

                    //oPayment.SendExtend.StoreExpireDate = 3; //超商繳費截止時間 CVS:以分鐘為單位 BARCODE:以天為單位
                    //oPayment.SendExtend.Desc_1 = "test1";//交易描述 1
                    //oPayment.SendExtend.Desc_2 = "test2";//交易描述 2
                    //oPayment.SendExtend.Desc_3 = "test3";//交易描述 3
                    //oPayment.SendExtend.Desc_4 = "";//交易描述 4
                    //oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
                    //oPayment.SendExtend.ClientRedirectURL = "";///Client 端回傳付款相關資訊

                    #endregion

                    /***************************信用卡額外功能參數***************************/

                    #region Credit 功能參數

                    //oPayment.SendExtend.BindingCard = BindingCardType.No; //記憶卡號
                    //oPayment.SendExtend.MerchantMemberID = ""; //記憶卡號識別碼
                    //oPayment.SendExtend.Language = "ENG"; //語系設定

                    #endregion Credit 功能參數

                    #region 一次付清

                    //oPayment.SendExtend.Redeem = false;   //是否使用紅利折抵
                    //oPayment.SendExtend.UnionPay = true; //是否為銀聯卡交易

                    #endregion

                    #region 分期付款

                    //oPayment.SendExtend.CreditInstallment = 3;//刷卡分期期數

                    #endregion 分期付款

                    #region 定期定額

                    //oPayment.SendExtend.PeriodAmount = 1000;//每次授權金額
                    //oPayment.SendExtend.PeriodType = PeriodType.Day;//週期種類
                    //oPayment.SendExtend.Frequency = 1;//執行頻率
                    //oPayment.SendExtend.ExecTimes = 2;//執行次數
                    //oPayment.SendExtend.PeriodReturnURL = "";//伺服器端回傳定期定額的執行結果網址。

                    #endregion

                    var result = oPayment.CheckOutString(ref html);
                    /* 產生訂單 */
                    enErrors.AddRange(oPayment.CheckOut());

                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }
            finally
            {
                // 顯示錯誤訊息。
                if (enErrors.Count() > 0)
                {
                    // string szErrorMessage = String.Join("\\r\\n", enErrors);
                }
            }

            return html;
        }

        public List<RentalConfirmViewModel> GetByOrderId(int? orderId)
        {
            var result = (
                from ExhibitionOrder in _DbRepository.GetAll<ExhibitionOrder>()
                where ExhibitionOrder.ExOrderId == orderId
                select new RentalConfirmViewModel()
                {
                    ExOrderId = ExhibitionOrder.ExOrderId,
                }
            ).ToList();
            return result;
        }
        public string GetPaymentResult(string RtnCode)
        {
            string paymentResultText = "已刷卡";

            if (RtnCode == "1")
            {
                paymentResultText = "已刷卡";
            }
            else if (RtnCode != "1")
            {
                paymentResultText = "交易失敗";
            }

            return paymentResultText;
        }

        public void SetState(RentalConfirmViewModel orderVM, string RtnCode)
        {
            if (RtnCode == "1")
            {
                var order = _DbRepository.GetAll<ExhibitionOrder>()
                            .Where(x => x.ExOrderId == orderVM.ExOrderId)
                            .FirstOrDefault();
                order.PaymentState = "已付款";
                _DbRepository.Update<ExhibitionOrder>(order);

            }

        }
    }
    
}