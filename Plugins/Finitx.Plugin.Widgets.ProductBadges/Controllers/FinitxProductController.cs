using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finitx.Plugin.Widgets.ProductBadges.Models;
using Finitx.Plugin.Widgets.ProductBadges.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Discounts;

using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Finitx.Plugin.Widgets.ProductBadges.Controllers
{
    /// <summary>
    /// not used
    /// </summary>
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class FinitxProductController : BasePluginController
    {
        private readonly IFinitxProductService _finitxProductService;
        public FinitxProductController(IFinitxProductService finitxProductService)
        {
            _finitxProductService= finitxProductService;
        }
        [HttpPost]
        public async Task<IActionResult> SaveProductFeature(FinitxProductDataModel  dataModel)
        {
            var saveResult=await _finitxProductService.SaveProductFeature(dataModel);

            if (saveResult.Item2)
                saveResult = await _finitxProductService.SaveProductIsComingSoon(dataModel);

            return new JsonResult(new { isSuccess=saveResult.Item2,message=saveResult.Item1});
        }
    }
}