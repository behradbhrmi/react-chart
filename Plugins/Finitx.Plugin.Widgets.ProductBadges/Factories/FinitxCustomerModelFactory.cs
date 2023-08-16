using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Gdpr;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Services.Authentication.External;
using Nop.Services.Authentication.MultiFactor;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Gdpr;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Factories;
using Nop.Web.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Widgets.ProductBadges.Factories
{
    public class FinitxCustomerModelFactory : CustomerModelFactory
    {
       
        private readonly AddressSettings _addressSettings;
        private readonly CaptchaSettings _captchaSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly CommonSettings _commonSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly ForumSettings _forumSettings;
        private readonly GdprSettings _gdprSettings;
        private readonly IAddressModelFactory _addressModelFactory;
        private readonly IAuthenticationPluginManager _authenticationPluginManager;
        private readonly ICountryService _countryService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICustomerService _customerService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IExternalAuthenticationService _externalAuthenticationService;
        private readonly IGdprService _gdprService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IMultiFactorAuthenticationPluginManager _multiFactorAuthenticationPluginManager;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IOrderService _orderService;
        private readonly IPictureService _pictureService;
        private readonly IProductService _productService;
        private readonly IReturnRequestService _returnRequestService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWorkContext _workContext;
        private readonly MediaSettings _mediaSettings;
        private readonly OrderSettings _orderSettings;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly TaxSettings _taxSettings;
        private readonly VendorSettings _vendorSettings;
        public FinitxCustomerModelFactory(AddressSettings addressSettings, CaptchaSettings captchaSettings, CatalogSettings catalogSettings, CommonSettings commonSettings, CustomerSettings customerSettings, DateTimeSettings dateTimeSettings, ExternalAuthenticationSettings externalAuthenticationSettings, ForumSettings forumSettings, GdprSettings gdprSettings, IAddressModelFactory addressModelFactory, IAuthenticationPluginManager authenticationPluginManager, ICountryService countryService, ICustomerAttributeParser customerAttributeParser, ICustomerAttributeService customerAttributeService, ICustomerService customerService, IDateTimeHelper dateTimeHelper, IExternalAuthenticationService externalAuthenticationService, IGdprService gdprService, IGenericAttributeService genericAttributeService, ILocalizationService localizationService, IMultiFactorAuthenticationPluginManager multiFactorAuthenticationPluginManager, INewsLetterSubscriptionService newsLetterSubscriptionService, IOrderService orderService, IPictureService pictureService, IProductService productService, IReturnRequestService returnRequestService, IStateProvinceService stateProvinceService, IStoreContext storeContext, IStoreMappingService storeMappingService, IUrlRecordService urlRecordService, IWorkContext workContext, MediaSettings mediaSettings, OrderSettings orderSettings, RewardPointsSettings rewardPointsSettings, SecuritySettings securitySettings, TaxSettings taxSettings, VendorSettings vendorSettings) : base(addressSettings, captchaSettings, catalogSettings, commonSettings, customerSettings, dateTimeSettings, externalAuthenticationSettings, forumSettings, gdprSettings, addressModelFactory, authenticationPluginManager, countryService, customerAttributeParser, customerAttributeService, customerService, dateTimeHelper, externalAuthenticationService, gdprService, genericAttributeService, localizationService, multiFactorAuthenticationPluginManager, newsLetterSubscriptionService, orderService, pictureService, productService, returnRequestService, stateProvinceService, storeContext, storeMappingService, urlRecordService, workContext, mediaSettings, orderSettings, rewardPointsSettings, securitySettings, taxSettings, vendorSettings)
        {
            _addressSettings = addressSettings;
            _captchaSettings = captchaSettings;
            _catalogSettings = catalogSettings;
            _commonSettings = commonSettings;
            _customerSettings = customerSettings;
            _dateTimeSettings = dateTimeSettings;
            _externalAuthenticationService = externalAuthenticationService;
            _externalAuthenticationSettings = externalAuthenticationSettings;
            _forumSettings = forumSettings;
            _gdprSettings = gdprSettings;
            _addressModelFactory = addressModelFactory;
            _authenticationPluginManager = authenticationPluginManager;
            _countryService = countryService;
            _customerAttributeParser = customerAttributeParser;
            _customerAttributeService = customerAttributeService;
            _customerService = customerService;
            _dateTimeHelper = dateTimeHelper;
            _gdprService = gdprService;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _multiFactorAuthenticationPluginManager = multiFactorAuthenticationPluginManager;
            _newsLetterSubscriptionService = newsLetterSubscriptionService;
            _orderService = orderService;
            _pictureService = pictureService;
            _productService = productService;
            _returnRequestService = returnRequestService;
            _stateProvinceService = stateProvinceService;
            _storeContext = storeContext;
            _storeMappingService = storeMappingService;
            _urlRecordService = urlRecordService;
            _workContext = workContext;
            _mediaSettings = mediaSettings;
            _orderSettings = orderSettings;
            _rewardPointsSettings = rewardPointsSettings;
            _securitySettings = securitySettings;
            _taxSettings = taxSettings;
            _vendorSettings = vendorSettings;
        }
        /// <summary>
        /// Prepare the customer navigation model
        /// </summary>
        /// <param name="selectedTabId">Identifier of the selected tab</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the customer navigation model
        /// </returns>
        public override async Task<CustomerNavigationModel> PrepareCustomerNavigationModelAsync(int selectedTabId = 0)
        {
            var model = new CustomerNavigationModel();

            model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
            {
                RouteName = "CustomerInfo",
                Title = await _localizationService.GetResourceAsync("Account.CustomerInfo"),
                Tab = CustomerNavigationEnum.Info,
                ItemClass = "customer-info"
            });

            //model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
            //{
            //    RouteName = "CustomerAddresses",
            //    Title = await _localizationService.GetResourceAsync("Account.CustomerAddresses"),
            //    Tab = CustomerNavigationEnum.Addresses,
            //    ItemClass = "customer-addresses"
            //});

            model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
            {
                RouteName = "CustomerOrders",
                Title = await _localizationService.GetResourceAsync("Account.CustomerOrders"),
                Tab = CustomerNavigationEnum.Orders,
                ItemClass = "customer-orders"
            });

            if (_orderSettings.ReturnRequestsEnabled &&
                (await _returnRequestService.SearchReturnRequestsAsync((await _storeContext.GetCurrentStoreAsync()).Id,
                    (await _workContext.GetCurrentCustomerAsync()).Id, pageIndex: 0, pageSize: 1)).Any())
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "CustomerReturnRequests",
                    Title = await _localizationService.GetResourceAsync("Account.CustomerReturnRequests"),
                    Tab = CustomerNavigationEnum.ReturnRequests,
                    ItemClass = "return-requests"
                });
            }

            if (!_customerSettings.HideDownloadableProductsTab)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "CustomerDownloadableProducts",
                    Title = await _localizationService.GetResourceAsync("Account.DownloadableProducts"),
                    Tab = CustomerNavigationEnum.DownloadableProducts,
                    ItemClass = "downloadable-products"
                });
            }

            if (!_customerSettings.HideBackInStockSubscriptionsTab)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "CustomerBackInStockSubscriptions",
                    Title = await _localizationService.GetResourceAsync("Account.BackInStockSubscriptions"),
                    Tab = CustomerNavigationEnum.BackInStockSubscriptions,
                    ItemClass = "back-in-stock-subscriptions"
                });
            }

            if (_rewardPointsSettings.Enabled)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "CustomerRewardPoints",
                    Title = await _localizationService.GetResourceAsync("Account.RewardPoints"),
                    Tab = CustomerNavigationEnum.RewardPoints,
                    ItemClass = "reward-points"
                });
            }
            model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
            {
                RouteName = "CustomerChangePassword",
                Title = await _localizationService.GetResourceAsync("Account.ChangePassword"),
                Tab = CustomerNavigationEnum.ChangePassword,
                ItemClass = "change-password"
            });

            if (_customerSettings.AllowCustomersToUploadAvatars)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "CustomerAvatar",
                    Title = await _localizationService.GetResourceAsync("Account.Avatar"),
                    Tab = CustomerNavigationEnum.Avatar,
                    ItemClass = "customer-avatar"
                });
            }

            if (_forumSettings.ForumsEnabled && _forumSettings.AllowCustomersToManageSubscriptions)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "CustomerForumSubscriptions",
                    Title = await _localizationService.GetResourceAsync("Account.ForumSubscriptions"),
                    Tab = CustomerNavigationEnum.ForumSubscriptions,
                    ItemClass = "forum-subscriptions"
                });
            }
            if (_catalogSettings.ShowProductReviewsTabOnAccountPage)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "CustomerProductReviews",
                    Title = await _localizationService.GetResourceAsync("Account.CustomerProductReviews"),
                    Tab = CustomerNavigationEnum.ProductReviews,
                    ItemClass = "customer-reviews"
                });
            }
            if (_vendorSettings.AllowVendorsToEditInfo && await _workContext.GetCurrentVendorAsync() != null)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "CustomerVendorInfo",
                    Title = await _localizationService.GetResourceAsync("Account.VendorInfo"),
                    Tab = CustomerNavigationEnum.VendorInfo,
                    ItemClass = "customer-vendor-info"
                });
            }
            if (_gdprSettings.GdprEnabled)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "GdprTools",
                    Title = await _localizationService.GetResourceAsync("Account.Gdpr"),
                    Tab = CustomerNavigationEnum.GdprTools,
                    ItemClass = "customer-gdpr"
                });
            }

            if (_captchaSettings.Enabled && _customerSettings.AllowCustomersToCheckGiftCardBalance)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "CheckGiftCardBalance",
                    Title = await _localizationService.GetResourceAsync("CheckGiftCardBalance"),
                    Tab = CustomerNavigationEnum.CheckGiftCardBalance,
                    ItemClass = "customer-check-gift-card-balance"
                });
            }

            if (await _multiFactorAuthenticationPluginManager.HasActivePluginsAsync())
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    RouteName = "MultiFactorAuthenticationSettings",
                    Title = await _localizationService.GetResourceAsync("PageTitle.MultiFactorAuthentication"),
                    Tab = CustomerNavigationEnum.MultiFactorAuthentication,
                    ItemClass = "customer-multiFactor-authentication"
                });
            }

            model.SelectedTab = (CustomerNavigationEnum)selectedTabId;

            return model;
        }

    }
}
