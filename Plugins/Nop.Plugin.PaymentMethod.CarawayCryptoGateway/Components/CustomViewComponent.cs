using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Components
{
    [ViewComponent(Name = "CarawayPaymentMethod")]
    public class PaymentZarinpalViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Plugins/PaymentMethod.CarawayCryptoGateway/Views/PaymentInfo.cshtml");
        }
    }
}
