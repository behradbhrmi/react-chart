using Finitx.Common.Helper;
using Finitx.Common.Models;
using Finitx.Plugin.Misc.LogEventsDataToServer.Dto;
using Finitx.Plugin.Misc.LogEventsDataToServer.ViewModel;
using Newtonsoft.Json;
using Nop.Core.Domain.Orders;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Services
{
    public class FinitxOrderService : IFinitxOrderService
    {
        IApiHelper _apiHelper;
        FinitxLogDataSetting _finitxLogDataSetting;
        public FinitxOrderService(IApiHelper apiHelper, FinitxLogDataSetting finitxLogDataSetting)
        {
            _apiHelper=apiHelper;
            _finitxLogDataSetting=finitxLogDataSetting; 
        }
        public async Task<(string, bool)> SendCompleteOrder(OrderCompleteRequestDto order)
        {
            try
            {
                var result=await _apiHelper.PostData<string, OrderCompleteVm>(_finitxLogDataSetting.PurchaseNotifyUrl, order.Data);
                return (result, result.ToLower().Contains("successfully"));
            }
            catch (Exception ex)
            {
                return (ex.Message,false);
            }
        }
    }
}
