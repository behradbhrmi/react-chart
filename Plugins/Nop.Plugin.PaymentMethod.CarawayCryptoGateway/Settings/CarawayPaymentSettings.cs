using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Settings
{
    public class CarawayPaymentSettings : ISettings
    {
        public decimal PriceStartToApplyInstantPayout { get; set; }
        public decimal PriceStartToApplyFastPayment { get; set; }
        public string PayoutAddress { get; set; }
        public string CallbackURL { get; set; }
        public string ApiKey { get; set; }
        public int DeadlineToPay { get; set; }
        public string PaymentOpenUrl { get; set; }
        public string PaymentVerifyUrl { get; set; }
        public decimal TetherRate { get; set; }

    }
    public enum PaymentMode
    {
        Standard=1,
        Fast=2
    }
}
