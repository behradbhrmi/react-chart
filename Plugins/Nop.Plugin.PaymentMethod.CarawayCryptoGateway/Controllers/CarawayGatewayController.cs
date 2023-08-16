using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Tax;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Models;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Services;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Settings;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Tax;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Controllers
{
    public class CarawayGatewayController : BasePaymentController
    {

        private readonly ISettingService _settingService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IPaymentPluginManager _paymentPluginManager;
        private readonly IPermissionService _permissionService;
        private readonly ILogger _logger;
        private readonly IStoreContext _storeContext;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly ICarawayPaymentService _carawayPaymentService;

        public CarawayPaymentSettings _settings { get; }

        private readonly ILocalizationService _localizationService;
        private INotificationService _notificationService;
        private CarawayPaymentSettings _gatewaySettings;
        public CarawayGatewayController(
            ICarawayPaymentService carawayPaymentService,
            CarawayPaymentSettings gatewaySettings,
            ILocalizationService localizationService,
            ISettingService settingService,
            IOrderProcessingService orderProcessingService,
            IOrderService orderService,
            IPaymentService paymentService,
            IPaymentPluginManager paymentPluginManager,
            IPermissionService permissionService,
            ILogger logger,
            IStoreContext storeContext,
            IWebHelper webHelper,
            IWorkContext workContext,
            ShoppingCartSettings shoppingCartSettings,
            CarawayPaymentSettings settings,
            INotificationService notificationService
            )
        {
            _carawayPaymentService = carawayPaymentService;
            _settings = settings;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _gatewaySettings = gatewaySettings;
            _settingService = settingService;
            _orderProcessingService = orderProcessingService;
            _orderService = orderService;
            _paymentService = paymentService;
            _paymentPluginManager = paymentPluginManager;
            _permissionService = permissionService;
            _logger = logger;
            _storeContext = storeContext;
            _webHelper = webHelper;
            _workContext = workContext;
            _shoppingCartSettings = shoppingCartSettings;
        }
        [AuthorizeAdmin] //confirms access to the admin panel
        [Area(AreaNames.Admin)] //specifies the area containing a controller or action        
        public async Task<IActionResult> Configure()
        {
            var configuration = AutoMapperConfiguration.Mapper.Map<CarawayConfigurationModel>(_gatewaySettings);
            return View("~/Plugins/PaymentMethod.CarawayCryptoGateway/Views/Configuration/Configure.cshtml", configuration);
        }
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Configure(CarawayConfigurationModel configurationModel)
        {
            var changedSetting = AutoMapperConfiguration.Mapper.Map<CarawayPaymentSettings>(configurationModel);

            await _settingService.SaveSettingAsync(changedSetting);
            await _settingService.ClearCacheAsync();

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return View("~/Plugins/PaymentMethod.CarawayCryptoGateway/Views/Configuration/Configure.cshtml", configurationModel);
        }

        public async Task<IActionResult> ResultHandler(string sessionid,string status, string OGUID)
        {
            if (await _paymentPluginManager.LoadPluginBySystemNameAsync("PaymentMethod.PaymentMethod.CarawayCryptoGateway") is not CarawayPaymentPlugin processor || !_paymentPluginManager.IsPluginActive(processor))
                throw new NopException("Caraway payment module cannot be loaded");
            if (sessionid == null)
            {
                throw new NopException("invalid callback parameters. sessionid is not provided");
            }
            Guid orderNumberGuid = Guid.Empty;
            try
            {
                orderNumberGuid = new Guid(OGUID);
            }
            catch { }

            var order = await _orderService.GetOrderByGuidAsync(orderNumberGuid);

                var verifyRsponse = await _carawayPaymentService.Verify(sessionid);
                var orderNote = new OrderNote()
                {
                    OrderId = order.Id,
                    Note = string.Concat(
                   "پرداخت ",
                  (status =="OK"? "" : "نا"), "موفق", " - ",
                      "پیغام درگاه : ", verifyRsponse.Message,
                    status == "OK" ? string.Concat(" - ", "کد پی گیری : ", verifyRsponse.TxId) : ""
                    ),
                    DisplayToCustomer = true,
                    CreatedOnUtc = DateTime.UtcNow
                };
            await _orderService.InsertOrderNoteAsync(orderNote);

            if (verifyRsponse.IsSuccess && _orderProcessingService.CanMarkOrderAsPaid(order))
            {
                order.AuthorizationTransactionId = verifyRsponse.TxId;
                await _orderService.UpdateOrderAsync(order);
                await _orderProcessingService.MarkOrderAsPaidAsync(order);
                return RedirectToRoute("CheckoutCompleted", new { orderId = order.Id });
            }
            return RedirectToRoute("orderdetails", new { orderId = order.Id });
          
        }
    }
}