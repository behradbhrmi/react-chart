using Finitx.Common.Models;
using Finitx.Plugin.Misc.LogEventsDataToServer.Domains;
using Finitx.Plugin.Misc.LogEventsDataToServer.Dto;
using Finitx.Plugin.Misc.LogEventsDataToServer.Services;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Controllers
{
    public class FinitxLogDataSettingController : BaseAdminController
    {
        private readonly ISettingService _settingService;
        private readonly FinitxLogDataSetting _finitxLogDataSetting;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IFinitxOrderService _finitxOrderService;
        private readonly IRepository<OrderSync> _orderSyncRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;

        public FinitxLogDataSettingController(
            ISettingService settingService,
            FinitxLogDataSetting finitxLogDataSetting,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IFinitxOrderService finitxOrderService,
            IRepository<OrderSync> orderSyncRepository,
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Customer> customerRepository,
            IRepository<Product> productRepository

            )
        {
            _settingService = settingService;
            _finitxLogDataSetting = finitxLogDataSetting;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _finitxOrderService = finitxOrderService;
            _orderSyncRepository = orderSyncRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        [AuthorizeAdmin] //confirms access to the admin panel
        [Area(AreaNames.Admin)] //specifies the area containing a controller or action        
        public async Task<IActionResult> Configure()
        {
            _finitxLogDataSetting.NeedOrderSync = await _orderSyncRepository.Table.AnyAsync(x => !x.HasSync);
            return View("~/Plugins/Finitx.Plugin.Misc.LogEventsDataToServer/Views/Configuration/Configure.cshtml", _finitxLogDataSetting);
        }
        [HttpGet]
        [AuthorizeAdmin] //confirms access to the admin panel
        [Area(AreaNames.Admin)] //specifies the area containing a controller or action        
        public async Task<IActionResult> SyncOrders()
        {
            try
            {
                var ordersNotSyncs = await _orderSyncRepository.Table.Where(x => !x.HasSync).ToListAsync();
                var success = 0;
                var failed = 0;
                foreach (var orderNotSync in ordersNotSyncs)
                {
                    var dto = await GetOrderCompleteRequestDto(orderNotSync.OrderItemGuid);
                    var resutl = await _finitxOrderService.SendCompleteOrder(dto);
                    orderNotSync.HasSync = resutl.Item2;
                    var add = resutl.Item2 ? (success++) : (failed++);
                    await _orderSyncRepository.UpdateAsync(orderNotSync);
                }
                _notificationService.SuccessNotification($"All:{ordersNotSyncs.Count} Success:{success} Failed:{failed}");
            }
            catch (Exception ex)
            {
                await _notificationService.ErrorNotificationAsync(ex);
            }

            return Json("ok");
        }
        private async Task<OrderCompleteRequestDto> GetOrderCompleteRequestDto(Guid orderItemGuid)
        {
            var orderItem = await _orderItemRepository.Table.FirstOrDefaultAsync(x => x.OrderItemGuid == orderItemGuid);
            var order = await _orderRepository.GetByIdAsync(orderItem.OrderId);
            var customer = await _customerRepository.GetByIdAsync(order.CustomerId);
            var product = await _productRepository.GetByIdAsync(orderItem.ProductId);

            return new OrderCompleteRequestDto
            {
                Data = new ViewModel.OrderCompleteVm
                {
                    CustomerEmail = customer.Email,
                    CustomerName = customer.SystemName,
                    OrderName = product.Name,
                    OrderSdk = product.Sku,
                    PurchasePrice = orderItem.PriceExclTax
                }
            };
        }
        [HttpPost]
        public async Task<IActionResult> Configure(FinitxLogDataSetting finitxLogDataSetting)
        {
            try
            {
                await _settingService.SaveSettingOverridablePerStoreAsync(finitxLogDataSetting, x => x.Username, true, 1, true);
                await _settingService.SaveSettingOverridablePerStoreAsync(finitxLogDataSetting, x => x.Password, true, 1, true);
                await _settingService.SaveSettingOverridablePerStoreAsync(finitxLogDataSetting, x => x.LoginToken, true, 1, true);
                await _settingService.SaveSettingOverridablePerStoreAsync(finitxLogDataSetting, x => x.LoginApiUrl, true, 1, true);
                await _settingService.SaveSettingOverridablePerStoreAsync(finitxLogDataSetting, x => x.PurchaseNotifyUrl, true, 1, true);
                await _settingService.ClearCacheAsync();
                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Finitx.Plugin.Misc.LogEventsDataToServer.Setting.SavedSuccessfully"));
            }
            catch (Exception ex)
            {
                await _notificationService.ErrorNotificationAsync(ex);
            }


            return View("~/Plugins/Finitx.Plugin.Misc.LogEventsDataToServer/Views/Configuration/Configure.cshtml", finitxLogDataSetting);
        }
    }
}
