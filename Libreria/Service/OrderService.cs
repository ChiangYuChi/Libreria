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
                LibreriaRepository repository = new LibreriaRepository();
                
                Order order = new Order()
                {
                    ShippingDate = DateTime.Now.AddDays(2),
                    OrderDate = DateTime.Now,
                    memberId = 1,
                    ShipName = orderVM.recipientName,
                    ShipCity = orderVM.addressCitySelect,
                    ShipRegion = orderVM.addressRegionSelect,
                    ShipAddress = orderVM.recipientAddress,
                    ShipPostalCode = orderVM.recipientPostalCode,
                    InvoiceType = orderVM.invoice,
                    InvoiceInfo = null,
                    CreateTime = DateTime.Now,
                    UpdateTime = null,
                    PaymentType = orderVM.paymentMethod,
                };

                repository.Create(order);

                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = 1,
                    Quantity = 2,
                };

                repository.Create(orderDetail);

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
            return _DbRepository.GetAll<Product>().Select(x => new OrderViewModel()
            {
                Id = x.ProductId,
                //Name = x.ProductName
            }).ToList();
        }
    }
}