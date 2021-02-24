using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class OrderService
    {
        private readonly LibreriaRepository _DbRepository;
        private readonly ShoppingService _shoppingService;

        public OrderService()
        {
            _DbRepository = new LibreriaRepository();
            _shoppingService = new ShoppingService();
        }

        public OperationResult Create(OrderViewModel orderVM)
        {
            int UserMemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            var result = new OperationResult();
            try
            {
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

                foreach(var orderDetailVM in orderVM.OrderDetailList)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = orderDetailVM.ProductId,
                        Quantity = orderDetailVM.Quantity,
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
                    Progress = "準備出貨中",
                    OrderPrice = 0,
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
                    Progress = "準備出貨中",
                    OrderPrice = 0,
                }
            ).ToList();

            foreach(var orderVM in result)
            {
                orderVM.OrderDetailList = GetOrderDetailByOrderId(orderVM.OrderId);
                orderVM.Progress = CalculateProgress(orderVM.OrderDate);
                orderVM.OrderPrice = CalculateOrderPrice(orderVM);
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
                    Progress = "準備出貨中",
                    OrderPrice = 0,
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
                    Progress = "準備出貨中",
                    OrderPrice = 0,
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
                    Progress = "準備出貨中",
                    OrderPrice = 0,
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
                    DetailPrice = 0,
                }
            ).ToList();

            foreach(var orderDetailVM in result)
            {
                CompleteOrderDetailVM(orderDetailVM);
            }

            return result;
        }

        public List<OrderDetailViewModel> GetOrderDetailByOrderId(int OrderId)
        {
            var result = (
                from orderDetail in _DbRepository.GetAll<OrderDetail>()
                join product in _DbRepository.GetAll<Product>()
                on orderDetail.ProductId equals product.ProductId
                where orderDetail.OrderId == OrderId
                select new OrderDetailViewModel()
                {
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    Quantity = orderDetail.Quantity,
                    DetailPrice = 0,
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

            return orderVM;
        }

        public OrderDetailViewModel CompleteOrderDetailVM(OrderDetailViewModel orderDetailVM)
        {
            orderDetailVM.DetailPrice = orderDetailVM.UnitPrice * orderDetailVM.Quantity;

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
                _shoppingService.DeleteFromCart(shoppingCartVM); //從購物車刪除該項
            }

            return orderVM;
        }

        public string CalculateProgress(DateTime orderDate)
        {
            if(DateTime.Now - orderDate < TimeSpan.FromDays(1))
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
            foreach(var orderDetailVM in orderVM.OrderDetailList)
            {
                orderVM.OrderPrice += orderDetailVM.DetailPrice;
            }

            return orderVM.OrderPrice;
        }
    }
}