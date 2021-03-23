using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECPay.Payment.Integration;

namespace Libreria.Service
{
    public class OrderService
    {
        private readonly LibreriaRepository _DbRepository;
        private readonly ShoppingService _shoppingService;
        private readonly ProductService _productService;

        public OrderService()
        {
            _DbRepository = new LibreriaRepository();
            _shoppingService = new ShoppingService();
            _productService = new ProductService();
        }

        public OperationResult Create(OrderViewModel orderVM)
        {
            int UserMemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            var result = new OperationResult();
            try
            {
                foreach (var orderDetailVM in orderVM.OrderDetailList)
                {
                    ProductViewModel productVM = _productService.GetById(orderDetailVM.ProductId);
                    productVM.Count -= orderDetailVM.Quantity;
                    _productService.Edit(productVM);
                }

                Order order = new Order()
                {
                    OrderDate = DateTime.Now,
                    ShippingDate = DateTime.Now.AddDays(1),
                    memberId = UserMemberId,
                    ShipName = orderVM.RecipientName,
                    ShipCity = orderVM.AddressCitySelect,
                    ShipRegion = orderVM.AddressRegionSelect,
                    ShipAddress = orderVM.RecipientAddress,
                    ShipPostalCode = orderVM.RecipientPostalCode,
                    InvoiceType = orderVM.Invoice,
                    InvoiceInfo = null,
                    CreateTime = DateTime.Now,
                    UpdateTime = null,
                    PaymentType = orderVM.PaymentMethod,
                };

                _DbRepository.Create(order);

                foreach (var orderDetailVM in orderVM.OrderDetailList)
                {
                    decimal specialPrice = 0;
                    //判斷特價
                    if (_DbRepository.GetAll<Product>().FirstOrDefault(product => product.ProductId == orderDetailVM.ProductId).isSpecial)
                    {
                        specialPrice = _DbRepository.GetAll<Product>().FirstOrDefault(product => product.ProductId == orderDetailVM.ProductId).SpecialPrice;
                    }
                    else specialPrice = 0;

                    //建立訂單詳細
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = orderDetailVM.ProductId,
                        Quantity = orderDetailVM.Quantity,
                        SpecialPrice = specialPrice,
                    };

                    _DbRepository.Create(orderDetail);
                }

                orderVM.OrderId = order.OrderId;

                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.exception = ex;
            }
            finally
            {

            }

            return result;
        }

        public List<OrderViewModel> GetAll()
        {
            var result = (
                from order in _DbRepository.GetAll<Order>()
                select new OrderViewModel()
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ShippingDate = order.ShippingDate,
                    PaymentMethod = order.PaymentType,
                    PaymentMethodText = null, //CompleteOrderVM(orderVM)
                    RecipientName = order.ShipName,
                    RecipientCellphone = null,
                    RecipientTelephone = null,
                    AddressCitySelect = order.ShipCity,
                    AddressRegionSelect = order.ShipRegion,
                    RecipientAddress = order.ShipAddress,
                    RecipientPostalCode = order.ShipPostalCode,
                    SubscriberName = null,
                    SubscriberCellphone = null,
                    SubscriberTelephone = null,
                    SubscriberAddress = null,
                    //OrderDetailList = null, //CompleteOrderVM(orderVM)
                    Progress = null, //CompleteOrderVM(orderVM)
                    OrderPrice = 0, //CompleteOrderVM(orderVM)
                }
            ).ToList();

            foreach (var orderVM in result)
            {
                CompleteOrderVM(orderVM);
            }

            return result;
        }

        public List<OrderViewModel> GetBymemberId(int memberId)
        {
            var result = (
                from order in _DbRepository.GetAll<Order>()
                where order.memberId == memberId
                select new OrderViewModel()
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ShippingDate = order.ShippingDate,
                    PaymentMethod = order.PaymentType,
                    PaymentMethodText = null, //CompleteOrderVM(orderVM)
                    RecipientName = order.ShipName,
                    RecipientCellphone = null,
                    RecipientTelephone = null,
                    AddressCitySelect = order.ShipCity,
                    AddressRegionSelect = order.ShipRegion,
                    RecipientAddress = order.ShipAddress,
                    RecipientPostalCode = order.ShipPostalCode,
                    SubscriberName = null,
                    SubscriberCellphone = null,
                    SubscriberTelephone = null,
                    SubscriberAddress = null,
                    //OrderDetailList = null, //CompleteOrderVM(orderVM)
                    Progress = null, //CompleteOrderVM(orderVM)
                    OrderPrice = 0, //CompleteOrderVM(orderVM)
                }
            ).ToList();

            foreach (var orderVM in result)
            {
                CompleteOrderVM(orderVM);
            }

            return result;
        }

        public List<OrderViewModel> GetBymemberId(int memberId, TimeSpan timeSpan)
        {
            if (timeSpan == null)
            {
                timeSpan = TimeSpan.MaxValue;
            }

            DateTime requiredDate = DateTime.Now - timeSpan;

            var result = (
                from order in _DbRepository.GetAll<Order>()
                where order.memberId == memberId &&
                    order.OrderDate > requiredDate
                select new OrderViewModel()
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ShippingDate = order.ShippingDate,
                    PaymentMethod = order.PaymentType,
                    PaymentMethodText = null, //CompleteOrderVM(orderVM)
                    RecipientName = order.ShipName,
                    RecipientCellphone = null,
                    RecipientTelephone = null,
                    AddressCitySelect = order.ShipCity,
                    AddressRegionSelect = order.ShipRegion,
                    RecipientAddress = order.ShipAddress,
                    RecipientPostalCode = order.ShipPostalCode,
                    SubscriberName = null,
                    SubscriberCellphone = null,
                    SubscriberTelephone = null,
                    SubscriberAddress = null,
                    //OrderDetailList = null, //CompleteOrderVM(orderVM)
                    Progress = null, //CompleteOrderVM(orderVM)
                    OrderPrice = 0, //CompleteOrderVM(orderVM)
                }
            ).ToList();

            foreach (var orderVM in result)
            {
                CompleteOrderVM(orderVM);
            }

            return result;
        }

        public List<OrderViewModel> GetByProgress(int memberId, string progress)
        {
            TimeSpan startTimeSpan;
            TimeSpan endTimeSpan;

            if (progress == "準備出貨中")
            {
                startTimeSpan = TimeSpan.FromDays(0);
                endTimeSpan = TimeSpan.FromDays(1);
            }
            else if (progress == "已出貨,尚未送達")
            {
                startTimeSpan = TimeSpan.FromDays(1);
                endTimeSpan = TimeSpan.FromDays(5);
            }
            else if (progress == "貨已送達")
            {
                startTimeSpan = TimeSpan.FromDays(5);
                endTimeSpan = TimeSpan.MaxValue;
            }
            else
            {
                startTimeSpan = TimeSpan.FromDays(5);
                endTimeSpan = TimeSpan.MaxValue;
            }

            DateTime requiredstartDate = DateTime.Now - startTimeSpan;
            DateTime requiredendDate = DateTime.Now - endTimeSpan;

            var result = (
                from order in _DbRepository.GetAll<Order>()
                where order.memberId == memberId &&
                    order.OrderDate <= requiredstartDate &&
                    order.OrderDate > requiredendDate
                select new OrderViewModel()
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ShippingDate = order.ShippingDate,
                    PaymentMethod = order.PaymentType,
                    PaymentMethodText = null, //CompleteOrderVM(orderVM)
                    RecipientName = order.ShipName,
                    RecipientCellphone = null,
                    RecipientTelephone = null,
                    AddressCitySelect = order.ShipCity,
                    AddressRegionSelect = order.ShipRegion,
                    RecipientAddress = order.ShipAddress,
                    RecipientPostalCode = order.ShipPostalCode,
                    SubscriberName = null,
                    SubscriberCellphone = null,
                    SubscriberTelephone = null,
                    SubscriberAddress = null,
                    //OrderDetailList = null, //CompleteOrderVM(orderVM)
                    Progress = null, //CompleteOrderVM(orderVM)
                    OrderPrice = 0, //CompleteOrderVM(orderVM)
                }
            ).ToList();

            foreach (var orderVM in result)
            {
                CompleteOrderVM(orderVM);
            }

            return result;
        }

        public List<OrderViewModel> GetByOrderId(int? orderId)
        {
            var result = (
                from order in _DbRepository.GetAll<Order>()
                where order.OrderId == orderId
                select new OrderViewModel()
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ShippingDate = order.ShippingDate,
                    PaymentMethod = order.PaymentType,
                    PaymentMethodText = null, //CompleteOrderVM(orderVM)
                    RecipientName = order.ShipName,
                    RecipientCellphone = null,
                    RecipientTelephone = null,
                    AddressCitySelect = order.ShipCity,
                    AddressRegionSelect = order.ShipRegion,
                    RecipientAddress = order.ShipAddress,
                    RecipientPostalCode = order.ShipPostalCode,
                    SubscriberName = null,
                    SubscriberCellphone = null,
                    SubscriberTelephone = null,
                    SubscriberAddress = null,
                    //OrderDetailList = null, //CompleteOrderVM(orderVM)
                    Progress = null, //CompleteOrderVM(orderVM)
                    OrderPrice = 0, //CompleteOrderVM(orderVM)
                }
            ).ToList();

            foreach (var orderVM in result)
            {
                CompleteOrderVM(orderVM);
            }

            return result;
        }

        public List<OrderViewModel> GetByTimeSpan(TimeSpan timeSpan)
        {
            if (timeSpan == null)
            {
                timeSpan = TimeSpan.MaxValue;
            }

            DateTime requiredDate = DateTime.Now - timeSpan;

            var result = (
                from order in _DbRepository.GetAll<Order>()
                where order.OrderDate > requiredDate
                select new OrderViewModel()
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ShippingDate = order.ShippingDate,
                    PaymentMethod = order.PaymentType,
                    PaymentMethodText = null, //CompleteOrderVM(orderVM)
                    RecipientName = order.ShipName,
                    RecipientCellphone = null,
                    RecipientTelephone = null,
                    AddressCitySelect = order.ShipCity,
                    AddressRegionSelect = order.ShipRegion,
                    RecipientAddress = order.ShipAddress,
                    RecipientPostalCode = order.ShipPostalCode,
                    SubscriberName = null,
                    SubscriberCellphone = null,
                    SubscriberTelephone = null,
                    SubscriberAddress = null,
                    //OrderDetailList = null, //CompleteOrderVM(orderVM)
                    Progress = null, //CompleteOrderVM(orderVM)
                    OrderPrice = 0, //CompleteOrderVM(orderVM)
                }
            ).ToList();

            foreach (var orderVM in result)
            {
                CompleteOrderVM(orderVM);
            }

            return result;
        }




        public List<OrderDetailViewModel> GetAllOrderDetail()
        {
            var result = (
                from orderDetail in _DbRepository.GetAll<OrderDetail>()
                join product in _DbRepository.GetAll<Product>()
                on orderDetail.ProductId equals product.ProductId
                select new OrderDetailViewModel()
                {
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    Quantity = orderDetail.Quantity,
                    SpecialPrice = orderDetail.SpecialPrice,
                    DetailPrice = 0, //CompleteOrderDetailVM(orderDetailVM)
                }
            ).ToList();

            foreach (var orderDetailVM in result)
            {
                CompleteOrderDetailVM(orderDetailVM);
            }

            return result;
        }

        public List<OrderDetailViewModel> GetOrderDetailByOrderId(int orderId)
        {
            var result = (
                from orderDetail in _DbRepository.GetAll<OrderDetail>()
                join product in _DbRepository.GetAll<Product>()
                on orderDetail.ProductId equals product.ProductId
                where orderDetail.OrderId == orderId
                select new OrderDetailViewModel()
                {
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    Quantity = orderDetail.Quantity,
                    SpecialPrice = orderDetail.SpecialPrice,
                    DetailPrice = 0, //CompleteOrderDetailVM(orderDetailVM)
                }
            ).ToList();

            foreach (var orderDetailVM in result)
            {
                CompleteOrderDetailVM(orderDetailVM);
            }

            return result;
        }




        public OrderViewModel CompleteOrderVM(OrderViewModel orderVM)
        {
            foreach (var orderDetailVM in orderVM.OrderDetailList)
            {
                CompleteOrderDetailVM(orderDetailVM);
            }

            orderVM.OrderDetailList = GetOrderDetailByOrderId(orderVM.OrderId);
            orderVM.Progress = CalculateProgress(orderVM.OrderDate);
            orderVM.OrderPrice = CalculateOrderPrice(orderVM);
            orderVM.PaymentMethodText = GetPaymentMethodText(orderVM.PaymentMethod);

            return orderVM;
        }

        public OrderDetailViewModel CompleteOrderDetailVM(OrderDetailViewModel orderDetailVM)
        {
            if (orderDetailVM.SpecialPrice > 0) { orderDetailVM.DetailPrice = orderDetailVM.SpecialPrice * orderDetailVM.Quantity; }
            else { orderDetailVM.DetailPrice = orderDetailVM.UnitPrice * orderDetailVM.Quantity; }

            return orderDetailVM;
        }




        public OrderViewModel PutShoppingCartsToOrderVM(OrderViewModel orderVM)
        {
            //訂單編號
            orderVM.OrderDetailList = new List<OrderDetailViewModel>();

            //取得購物車
            var shoppingCartVMList = _shoppingService.GetAll();
            foreach (var shoppingCartVM in shoppingCartVMList)
            {
                OrderDetailViewModel orderDetailVM = new OrderDetailViewModel()
                {
                    ProductId = shoppingCartVM.ProductId,
                    Quantity = shoppingCartVM.Count,
                };
                orderVM.OrderDetailList.Add(orderDetailVM);
                _shoppingService.DeleteFromCart(shoppingCartVM.ProductId); //從購物車刪除該項
            }

            return orderVM;
        }

        public string CalculateProgress(DateTime orderDate)
        {
            if (DateTime.Now - orderDate < TimeSpan.FromDays(1))
            {
                return "準備出貨中";
            }
            else if (DateTime.Now - orderDate < TimeSpan.FromDays(5))
            {
                return "已出貨，尚未送達";
            }
            else
            {
                return "貨已送達";
            }
        }

        public decimal CalculateOrderPrice(OrderViewModel orderVM)
        {
            orderVM.OrderPrice = 0;
            foreach (var orderDetailVM in orderVM.OrderDetailList)
            {
                orderVM.OrderPrice += orderDetailVM.DetailPrice;
            }

            return orderVM.OrderPrice;
        }

        public string GetPaymentMethodText(int paymentMethod)
        {
            string paymentMethodText = "取貨付款";
            if (paymentMethod == 1)
            {
                paymentMethodText = "取貨付款";
            }
            else if (paymentMethod == 2)
            {
                paymentMethodText = "ATM轉帳";
            }
            else if (paymentMethod == 3)
            {
                paymentMethodText = "信用卡";
            }

            return paymentMethodText;
        }

        public string ECPay(OrderViewModel orderVM)
        {
            List<string> enErrors = new List<string>();
            string html = string.Empty;
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    var orderDetail = orderVM.OrderDetailList;
                    var orderTotal = orderVM.OrderPrice;

                    /* 服務參數 */
                    oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
                    oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
                    oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
                    oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
                    oPayment.MerchantID = "2000132";//ECPay提供的特店編號

                    /* 基本參數 */
                    oPayment.Send.ReturnURL = "https://weblibreria.azurewebsites.net/Order/PayReturnResult";//付款完成通知回傳的網址
                    oPayment.Send.ClientBackURL = "http://127.0.0.1:4040";//瀏覽器端返回的廠商網址
                    oPayment.Send.OrderResultURL = $"https://weblibreria.azurewebsites.net/Order/PayReturnDetail?orderId={orderVM.OrderId}";//瀏覽器端回傳付款結果網址
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

                    foreach (var item in orderDetail)
                    {
                        oPayment.Send.Items.Add(new Item()
                        {
                            Name = item.ProductName,//商品名稱
                            Price = Decimal.Parse($"{item.UnitPrice}".ToString()),//商品單價
                            Currency = "新台幣",//幣別單位
                            Quantity = Int32.Parse("1"),//購買數量
                            URL = "http://google.com",//商品的說明網址

                        });
                    }


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

        public void SetState(OrderViewModel orderVM, string RtnCode)
        {
            if (RtnCode == "1")
            {
                var order = _DbRepository.GetAll<Order>()
                            .Where(x => x.OrderId == orderVM.OrderId)
                            .FirstOrDefault();
                order.PaymentState = "已付款";
                _DbRepository.Update<Order>(order);

            }

        }
    }
}