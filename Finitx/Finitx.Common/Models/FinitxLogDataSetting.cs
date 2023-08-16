using Nop.Core.Configuration;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Models
{
    public class FinitxLogDataSetting:ISettings
    {
        [NopResourceDisplayName("Finitx.Plugin.Misc.LogEventsDataToServer.Setting.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Finitx.Plugin.Misc.LogEventsDataToServer.Setting.Password")]
        public string Password { get; set; }

        [NopResourceDisplayName("Finitx.Plugin.Misc.LogEventsDataToServer.Setting.LoginApiUrl")]
        public string LoginApiUrl { get; set; }

        [NopResourceDisplayName("Finitx.Plugin.Misc.LogEventsDataToServer.Setting.LoginToken")]
        public string LoginToken { get; set; }
        
        [NopResourceDisplayName("Finitx.Plugin.Misc.LogEventsDataToServer.Setting.PurchaseNotifyUrl")]
        public string PurchaseNotifyUrl { get; set; }
        [NopResourceDisplayName("Finitx.Plugin.Misc.LogEventsDataToServer.Setting.BaseUrl")]
        public string BaseUrl { get; set; } = "https://auth-hub.dpnaco.com/";
        public bool NeedOrderSync { get; set; }
    }
}
