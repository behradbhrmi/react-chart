using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Models
{
    public class CarawayConfigurationModel
    {
        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.PriceStartToApplyInstantPayout")]
        public decimal PriceStartToApplyInstantPayout { get; set; }

        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.PriceStartToApplyFastPayment")]
        public decimal PriceStartToApplyFastPayment { get; set; }

        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.PayoutAddress")]
        public string PayoutAddress { get; set; }

        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.CallbackURL")]
        public string CallbackURL { get; set; }

        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.InstantPayout")]
        public bool InstantPayout { get; set; }

        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.ApiKey")]
        public string ApiKey { get; set; }

        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.DeadlineToPay")]
        public int DeadlineToPay { get; set; }

        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.PaymentOpenUrl")]
        public string PaymentOpenUrl { get; set; }

        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.PaymentVerifyUrl")]
        public string PaymentVerifyUrl { get; set; }

        [NopResourceDisplayName("Nop.Plugin.PaymentMethod.TetherRate")]
        public decimal TetherRate { get; set; }

    }
}
