using Finitx.Common.Constants;
using Finitx.Plugin.Misc.LogEventsDataToServer.Domains;
using Finitx.Plugin.Misc.LogEventsDataToServer.Services;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Events
{
    public class OrderEvent : IConsumer<OrderPaidEvent>
    {

        IFinitxOrderService _finitxOrderService;
        IProductService _productService;
        ICustomerService _customerService;
        IRepository<OrderSync> _orderSyncRepository;
        IRepository<Order> _orderRepository;
        IGenericAttributeService _genericAttributeService;
        private readonly IRepository<OrderItem> _orderItemRepository;
        public OrderEvent(
            IFinitxOrderService finitxOrderService,
            IRepository<OrderItem> repository,
            IProductService productService,
            ICustomerService customerService,
            IRepository<OrderSync> orderSyncRepository,
            IRepository<Order> orderRepository,
             IGenericAttributeService genericAttributeService
            )
        {

            _finitxOrderService = finitxOrderService;
            _orderItemRepository = repository;
            _productService = productService;
            _customerService = customerService;
            _orderSyncRepository = orderSyncRepository;
            _orderRepository = orderRepository;
            _genericAttributeService = genericAttributeService;
        }
        /// <summary>
        /// در اینجا هنگامی که خرید کامل میشود اطلاعات خرید را به سرور ارسال میکنیم 
        /// </summary>
        /// <param name="eventMessage"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(OrderPaidEvent eventMessage)
        {
            var currerntUser = await _customerService.GetCustomerByIdAsync(eventMessage.Order.CustomerId);
            var orderItems = (await _orderItemRepository.GetAllAsync(x => x.Where(o => o.OrderId == eventMessage.Order.Id))).ToList();            
            foreach (var orderItem in orderItems)
            {
                var product = await _productService.GetProductByIdAsync(orderItem.ProductId);
                if (product != null)
                {
                    var firstName =await _genericAttributeService.GetAttributeAsync<string>(currerntUser, "FirstName");
                    var lastName = await _genericAttributeService.GetAttributeAsync<string>(currerntUser, "LastName");
                    var result = await _finitxOrderService.SendCompleteOrder(new Dto.OrderCompleteRequestDto
                    {
                        
                        Data = new ViewModel.OrderCompleteVm
                        {
                            CustomerEmail = currerntUser.Email,
                            CustomerName = $"{firstName} {lastName}",
                            OrderName = product.Name,
                            PurchasePrice = orderItem.PriceInclTax,
                            OrderSdk=product.Sku,    
                            Currency=eventMessage.Order.CustomerCurrencyCode,
                            Description=$@"
                                            Currency Rate:{eventMessage.Order.CurrencyRate}
                                            Customer IP: {eventMessage.Order.CustomerIp}
                                            Payment Method: {eventMessage.Order.PaymentMethodSystemName}
                                           "
                        }
                    });
                  await  _orderSyncRepository.InsertAsync(new OrderSync { HasSync = result.Item2, OrderGuid = eventMessage.Order.OrderGuid, OrderItemGuid = orderItem.OrderItemGuid });
                }
            }


        }
    }
}
