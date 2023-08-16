using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Services.RequestResponse
{
    public class VerifyResponseModel
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public PaymentStatusCode Code { get; set; }
        public string TxId { get; set; }

        [JsonProperty("paid_currency")]
        public string PaiedCurrency { get; set; } = "usdt_trc20";

        [JsonProperty("check_amount")]
        public decimal CheckAmount { get; set; }

        [JsonProperty("paid_amount")]
        public decimal PaiedAmount { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }
    }
    public enum PaymentStatusCode
    {
        WaitingForPayment = 0,
        Expired = -1,
        AlreadyVerified = 101,
        Failed = -2,
        NeedConfirmation = 1,
        Verified = 100,
        InProgeress = 2,
        Declined = -3
    }
}
