using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Services;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Services.RequestResponse;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Settings;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Plugins;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway
{
    /// <summary>
    /// Rename this file and change to the correct type
    /// </summary>
    public class CarawayPaymentPlugin : BasePlugin, IPaymentMethod
    {
        private readonly CustomerSettings _customerSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderService _orderService;
        private readonly ICarawayPaymentService _carawayPaymentService;
        private readonly IPaymentService _paymentService;
        private readonly CurrencySettings _currencySettings;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly ICurrencyService _currencyService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly ISettingService _settingService;
        private readonly ITaxService _taxService;
        private readonly IWebHelper _webHelper;
        private readonly CarawayPaymentSettings _carawayPalPaymentSettings;
        private readonly ILanguageService _languageService;
        private readonly IStoreService _storeService;
        private readonly ICustomerService _customerService;
        private IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IAddressService _addressService;
        public CarawayPaymentPlugin(CurrencySettings currencySettings,
            ICarawayPaymentService carawayPaymentService,
            IHttpContextAccessor httpContextAccessor,
            IPaymentService paymentService,
            ICheckoutAttributeParser checkoutAttributeParser,
            ICurrencyService currencyService,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            IOrderTotalCalculationService orderTotalCalculationService,
            ISettingService settingService,
            ITaxService taxService,
            IWebHelper webHelper,
            CarawayPaymentSettings carawayPalPaymentSettings,
            ILanguageService languageService,
            IStoreService storeService,
            ICustomerService customerService,
            IWorkContext workContext,
            IStoreContext storeContext,
            CustomerSettings customerSettings,
            IOrderService orderService,
            IAddressService addressService)
        {
            _orderService = orderService;
            _carawayPaymentService = carawayPaymentService;
            _paymentService = paymentService;
            _httpContextAccessor = httpContextAccessor;
            _workContext = workContext;
            _customerService = customerService;
            _storeService = storeService;
            _currencySettings = currencySettings;
            _checkoutAttributeParser = checkoutAttributeParser;
            _currencyService = currencyService;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _orderTotalCalculationService = orderTotalCalculationService;
            _settingService = settingService;
            _taxService = taxService;
            _webHelper = webHelper;
            _carawayPalPaymentSettings = carawayPalPaymentSettings;
            _storeContext = storeContext;
            _languageService = languageService;
            _customerSettings = customerSettings;
            _addressService = addressService;
        }
        public bool SupportCapture => false;

        public bool SupportPartiallyRefund => false;

        public bool SupportRefund => false;

        public bool SupportVoid => false;

        public RecurringPaymentType RecurringPaymentType => RecurringPaymentType.NotSupported;

        public PaymentMethodType PaymentMethodType => PaymentMethodType.Redirection;

        public bool SkipPaymentInfo => true;

        public Task<CancelRecurringPaymentResult> CancelRecurringPaymentAsync(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            var result = new CancelRecurringPaymentResult();
            result.AddError("Recurring payment not supported");
            return Task.FromResult(result);
        }

        public Task<bool> CanRePostProcessPaymentAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            //let's ensure that at least 5 seconds passed after order is placed
            //P.S. there's no any particular reason for that. we just do it
            if ((DateTime.UtcNow - order.CreatedOnUtc).TotalSeconds < 5)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public Task<CapturePaymentResult> CaptureAsync(CapturePaymentRequest capturePaymentRequest)
        {
            var result = new CapturePaymentResult();
            result.AddError("Capture method not supported");
            return Task.FromResult(result);
        }

        public Task<decimal> GetAdditionalHandlingFeeAsync(IList<ShoppingCartItem> cart)
        {
            return Task.FromResult<decimal>(0);
        }

        public Task<ProcessPaymentRequest> GetPaymentInfoAsync(IFormCollection form)
        {
            return Task.FromResult(new ProcessPaymentRequest());
        }

        public async Task<string> GetPaymentMethodDescriptionAsync()
        {
            return await _localizationService.GetResourceAsync("plugins.payments.caraway.PaymentMethodDescription");
        }

        public string GetPublicViewComponentName()
        {
            return "CarawayPaymentMethod";
        }

        public async Task<bool> HidePaymentMethodAsync(IList<ShoppingCartItem> cart)
        {
            bool hide = false;
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var carawayPaymentSettings = await _settingService.LoadSettingAsync<CarawayPaymentSettings>(storeId);
            hide = string.IsNullOrWhiteSpace(carawayPaymentSettings.ApiKey);
            return hide;
        }

        public async Task PostProcessPaymentAsync(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            var customer = await _customerService.GetCustomerByIdAsync(postProcessPaymentRequest.Order.CustomerId);
            var order = postProcessPaymentRequest.Order;
            var store = await _storeService.GetStoreByIdAsync(order.StoreId);
            var total = order.OrderTotal;// * usdCurrency.Rate;
            var primaryCurrency = await _currencyService.GetCurrencyByIdAsync(_currencySettings.PrimaryStoreCurrencyId);
            var usdCurrency = await _currencyService.GetCurrencyByCodeAsync("USD");            
            var total1 = order.OrderTotal * usdCurrency.Rate;
            total = Convert.ToInt32(await _currencyService.ConvertCurrencyAsync(total, primaryCurrency, usdCurrency));
            var orderItems = await _orderService.GetOrderItemsAsync(order.Id);
            string PhoneOfUser = String.Empty;
            var billingAddress = await _addressService.GetAddressByIdAsync(order.BillingAddressId);
            var shippingAddress = await _addressService.GetAddressByIdAsync(order.ShippingAddressId ?? 0);

            if (_customerSettings.PhoneEnabled)// Default Phone number of the Customer
                PhoneOfUser = await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.PhoneAttribute);
            if (string.IsNullOrEmpty(PhoneOfUser))//Phone number of the BillingAddress
                PhoneOfUser = billingAddress.PhoneNumber;
            if (string.IsNullOrEmpty(PhoneOfUser))//Phone number of the ShippingAddress
                PhoneOfUser = string.IsNullOrEmpty(shippingAddress?.PhoneNumber) ? PhoneOfUser : $"{PhoneOfUser} - {shippingAddress.PhoneNumber}";

            var Name = await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.FirstNameAttribute);
            var Family = await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.LastNameAttribute);
            string NameFamily = $"{Name ?? ""} {Family ?? ""}".Trim();

            var description = $"{store.Name}{(string.IsNullOrEmpty(NameFamily) ? "" : $" - {NameFamily}")} - {customer.Email}{(string.IsNullOrEmpty(PhoneOfUser) ? "" : $" - {PhoneOfUser}")}";
            var instantPayout = order.OrderTotal > _carawayPalPaymentSettings.PriceStartToApplyInstantPayout;
            var fastPayment = !instantPayout && order.OrderTotal > _carawayPalPaymentSettings.PriceStartToApplyFastPayment;
            try
            {
                var response = await _carawayPaymentService.CreatePayment(new CreatePaymentRequestModel()
                {
                    CallbackUrl = string.Concat(_webHelper.GetStoreLocation(), "CarawayGateway/ResultHandler", "?OGUId=" + postProcessPaymentRequest.Order.OrderGuid),
                    DeadlineToPay = _carawayPalPaymentSettings.DeadlineToPay,
                    Description = description,
                    InstantPayout = instantPayout,
                    Mode = fastPayment ? Services.RequestResponse.PaymentMode.Fast : Services.RequestResponse.PaymentMode.Standard,
                    OrderId = order.OrderGuid.ToString(),
                    PayoutAddress = _carawayPalPaymentSettings.PayoutAddress,
                    PriceInUsdt = total
                });
                _httpContextAccessor.HttpContext.Response.Redirect(response.PaymentUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.NewPaymentStatus = PaymentStatus.Pending;
            return Task.FromResult(result);
        }

        public Task<ProcessPaymentResult> ProcessRecurringPaymentAsync(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.AddError("Recurring payment not supported");
            return Task.FromResult(result);
        }

        public Task<RefundPaymentResult> RefundAsync(RefundPaymentRequest refundPaymentRequest)
        {
            var result = new RefundPaymentResult();
            result.AddError("Refund method not supported");
            return Task.FromResult(result);
        }

        public Task<IList<string>> ValidatePaymentFormAsync(IFormCollection form)
        {
            return Task.FromResult<IList<string>>(new List<string>());
        }

        public Task<VoidPaymentResult> VoidAsync(VoidPaymentRequest voidPaymentRequest)
        {
            var result = new VoidPaymentResult();
            result.AddError("Void method not supported");
            return Task.FromResult(result);
        }
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/CarawayGateway/Configure";
        }

        public override async Task InstallAsync()
        {
            await _localizationService.AddLocaleResourceAsync(new Dictionary<string, string>
            {
                ["plugins.payments.caraway.paymentmethoddescription"] = "Pay by Tether(USDT) TRC20 ",

                ["Nop.Plugin.PaymentMethod.PriceStartToApplyInstantPayout"] = "Enable instant payout for prices above",
                ["Nop.Plugin.PaymentMethod.PriceStartToApplyInstantPayout.Hint"] = "Based on store active currency.After receiving money on the pay-in account gateway is not going to transfer the money to the recipient wallet instantly. Instead, it will balance the recipient account at the gateway dashboard.Afterward the recipient will be able to withdraw his assets one time with less paying the network fee (1 USDT)",

                ["Nop.Plugin.PaymentMethod.PriceStartToApplyFastPayment"] = "Enable fast payment for prices below",
                ["Nop.Plugin.PaymentMethod.PriceStartToApplyFastPayment.Hint"] = "In fast payments, the gateway will not wait for network confirmations and will redirect the customer to the store after receiving the desired amount of money This mode is not recommended for high amounts of money.",

                ["Nop.Plugin.PaymentMethod.PayoutAddress"] = "Payout address",
                ["Nop.Plugin.PaymentMethod.PayoutAddress.Hint"] = "This is the wallet address you provide to transfer money or balance after successful payment. If not provided you must have an active wallet on your account The address should be a Tron address.",

                ["Nop.Plugin.PaymentMethod.InstantPayout"] = "Instant payout",
                ["Nop.Plugin.PaymentMethod.InstantPayout.Hint"] = "The money will transfer immediately to payout address as soon as gateway received the money ",

                ["Nop.Plugin.PaymentMethod.ApiKey"] = "API key",
                ["Nop.Plugin.PaymentMethod.ApiKey.Hint"] = "Do not ALTER this if you are not aware of what you are doing",

                ["Nop.Plugin.PaymentMethod.DeadlineToPay"] = "Payment deadline (minutes)",
                ["Nop.Plugin.PaymentMethod.DeadlineToPay.Hint"] = "The amount of time which a customer have on payment page to finish payment",

                ["Nop.Plugin.PaymentMethod.PaymentOpenUrl"] = "Open URL",
                ["Nop.Plugin.PaymentMethod.PaymentOpenUrl.Hint"] = "Do not ALTER this if you are not aware of what you are doing",

                ["Nop.Plugin.PaymentMethod.PaymentVerifyUrl"] = "Veify URL",
                ["Nop.Plugin.PaymentMethod.PaymentVerifyUrl.Hint"] = "Do not ALTER this if you are not aware of what you are doing",

                ["Nop.Plugin.PaymentMethod.TetherRate"] = "Tether price",
                ["Nop.Plugin.PaymentMethod.TetherRate.Hint"] = "Tether price according to store primary currency",

                ["plugins.payments.caraway.PaymentMethodDescription"] = "Pay with Tether (TRC20)",
                ["plugins.payments.caraway.redirectiontip"] = "You will continue to Caraway payment page."

            }, 1);
            await base.InstallAsync();
        }
    }
}
