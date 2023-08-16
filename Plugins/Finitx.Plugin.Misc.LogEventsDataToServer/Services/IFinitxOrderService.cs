using Finitx.Plugin.Misc.LogEventsDataToServer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Services
{
    public interface IFinitxOrderService
    {
        Task<(string,bool)> SendCompleteOrder(OrderCompleteRequestDto order);
    }
}
