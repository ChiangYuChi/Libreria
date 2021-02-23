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
                    ShippingDate = DateTime.Now.AddDays(1),
                    OrderDate = DateTime.Now,
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
                }
            ).ToList();

            foreach (var order in result)
            {
                order.OrderDetailList = GetOrderDetailByOrderId(order.OrderId);
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
                }
            ).ToList();

            foreach(var order in result)
            {
                order.OrderDetailList = GetOrderDetailByOrderId(order.OrderId);
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
                }
            ).ToList();

            foreach (var order in result)
            {
                order.OrderDetailList = GetOrderDetailByOrderId(order.OrderId);
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
                }
            ).ToList();

            foreach (var order in result)
            {
                order.OrderDetailList = GetOrderDetailByOrderId(order.OrderId);
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
                }
            ).ToList();

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
                }
            ).ToList();

            return result;
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
                    Quantity = shoppingCartVM.Count
                };
                orderVM.OrderDetailList.Add(orderDetailVM);
                _shoppingService.DeleteFromCart(shoppingCartVM); //從購物車刪除該項
            }

            return orderVM;
        }

    }
}