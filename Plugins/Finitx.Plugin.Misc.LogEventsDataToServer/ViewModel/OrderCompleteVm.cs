using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.ViewModel
{
    public class OrderCompleteVm
    {
        [JsonProperty("orderSdk")]
        public string OrderSdk { get; set; }
        [JsonProperty("orderName")]
        public string OrderName { get; set; }
        [JsonProperty("customerEmail")]
        public string CustomerEmail { get; set; }
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }
        [JsonProperty("purchasePrice")]
        public decimal PurchasePrice { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
