using Finitx.Plugin.Widgets.ProductBadges.Models;
using Finitx.Plugin.Widgets.ProductBadges.Services;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;
using System;
using System.Threading.Tasks;

namespace Finitx.Plugin.Widgets.ProductBadges.Components
{
    [ViewComponent(Name = ProductBadgesDefaults.MIDDLE_BADGE_VIEW_COMPONENT_NAME)]
    public class ProductMiddleBadgeViewComponent : NopViewComponent
    {
        private readonly IFinitxProductService _finitxProductService;
        public ProductMiddleBadgeViewComponent(IFinitxProductService finitxProductService)
        {
            _finitxProductService = finitxProductService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            return Content(string.Empty);
            if (widgetZone != PublicWidgetZones.ProductBoxAddinfoMiddle)
            {
                return Content(string.Empty);
            }
            var productOverView = additionalData as ProductOverviewModel;
            
            var model = new FinitxProductDataModel();
            if (productOverView != null)
            {
                var featuers=await _finitxProductService.GetProductFeatures(productOverView.Id);
                model.FeaturesList = featuers;
            }
            return View("~/Plugins/Finitx.Plugin.Widgets.ProductBadges/Views/ProductMiddleBadgeView.cshtml",model);
        }
    }
}
