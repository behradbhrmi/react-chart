using Finitx.Plugin.Widgets.ProductBadges.Models;
using Finitx.Plugin.Widgets.ProductBadges.Services;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Widgets.ProductBadges.Components
{
    [ViewComponent(Name = ProductBadgesDefaults.FINITX_Product_Data_VIEW_COMPONENT_NAME)]
    public class FinitxProductDataViewComponent:NopViewComponent
    {
        private readonly IFinitxProductService _finitxProductService;
        public FinitxProductDataViewComponent(IFinitxProductService finitxProductService)
        {
            _finitxProductService= finitxProductService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (widgetZone != AdminWidgetZones.ProductDetailsBlock)
            {
                return Content(string.Empty);
            }
            var productModel = additionalData as ProductModel;
            var model = new FinitxProductDataModel();
            model.FeaturesList =await _finitxProductService.GetProductFeatures(productModel.Id);
            model.Features =string.Join('\n', await _finitxProductService.GetProductFeatures(productModel.Id)??new List<string>());
            model.IsComingSoon= await _finitxProductService.GetProductIsComingSoon(productModel.Id);
            return View("~/Plugins/Finitx.Plugin.Widgets.ProductBadges/Views/FinitxProductDataView.cshtml",model);
        }
    }
}
