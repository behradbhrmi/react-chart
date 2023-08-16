using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Services.RequestResponse
{
    public class CreatePaymentResponseModel
    {
        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("payment_url")]
        public string PaymentUrl { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }
    }
}
