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

        public OrderService()
        {
            _DbRepository = new LibreriaRepository();
        }

        public OperationResult Create(OrderViewModel orderVM)
        {
            var result = new OperationResult();
            try
            {
                Order order = new Order()
                {
                    ShippingDate = DateTime.Now.AddDays(2),
                    OrderDate = DateTime.Now,
                    memberId = 1,
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

                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = 1,
                    Quantity = 2,
                };

                _DbRepository.Create(orderDetail);

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

                    OrderDetailVMList = GetOrderDetailByOrderId(order.OrderId)
                }
            ).ToList();

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

                    OrderDetailVMList = GetOrderDetailByOrderId(order.OrderId),
                }
            ).ToList();

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
                    Progress = null
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
                    Progress = null
                }
            ).ToList();

            return result;
        }

    }
}