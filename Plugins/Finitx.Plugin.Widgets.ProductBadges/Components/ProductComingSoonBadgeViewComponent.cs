using Finitx.Plugin.Widgets.ProductBadges.Models;
using Finitx.Plugin.Widgets.ProductBadges.Services;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Catalog;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;
using System;
using System.Threading.Tasks;

namespace Finitx.Plugin.Widgets.ProductBadges.Components
{
    [ViewComponent(Name = ProductBadgesDefaults.BEFORE_BADGE_VIEW_COMPONENT_NAME)]
    public class ProductComingSoonBadgeViewComponent : NopViewComponent
    {
        private readonly IFinitxProductService _finitxProductService;
        private readonly IProductService _productService;
        public ProductComingSoonBadgeViewComponent(
            IFinitxProductService finitxProductService,
            IProductService productService
            )
        {
            _finitxProductService = finitxProductService;
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (widgetZone != PublicWidgetZones.ProductBoxAddinfoBefore)
            {
                return Content(string.Empty);
            }
            var productOverView = additionalData as ProductOverviewModel;
            
            var model = new FinitxProductDataModel();
            if (productOverView != null)
            {
                var featuers=await _finitxProductService.GetProductFeatures(productOverView.Id);
                model.FeaturesList = featuers;
                model.IsComingSoon =(await _productService.GetProductByIdAsync(productOverView.Id)).StockQuantity<=0;
            }
            return View("~/Plugins/Finitx.Plugin.Widgets.ProductBadges/Views/ProductBeforeBadgeView.cshtml", model);
        }
    }
}
