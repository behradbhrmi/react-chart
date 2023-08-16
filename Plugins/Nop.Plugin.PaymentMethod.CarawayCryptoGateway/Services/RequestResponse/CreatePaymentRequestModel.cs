using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Services.RequestResponse
{
    public class CreatePaymentRequestModel
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("price_in_usdt")]
        public decimal PriceInUsdt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("deadline_to_pay")]
        public int DeadlineToPay { get; set; } = 15;

        [JsonProperty("instant_payout")]
        public bool InstantPayout { get; set; } = false;

        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonProperty("mode")]
        public PaymentMode Mode { get; set; } 

        [JsonProperty("payout_address")]
        public string PayoutAddress { get; set; }
    }
    public enum PaymentMode
    {
        Standard = 1,
        Fast = 2
    }
}
