using Finitx.Common.Helper;
using Finitx.Common.Models;
using Finitx.Common.Service;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer
{
    /// <summary>
    /// Rename this file and change to the correct type
    /// </summary>
    public class LogEventsDataToServerPlugin : BasePlugin
    {
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly IResourceHelper _resourceHelper;

        public LogEventsDataToServerPlugin(ISettingService settingService, IWebHelper webHelper, IResourceHelper resourceHelper)
        {
            _settingService = settingService;
            _webHelper = webHelper;
            _resourceHelper= resourceHelper;
        }
        public override async Task InstallAsync()
        {
            await SaveDefaultSetting();
            await AdResources();
            await base.InstallAsync();
        }
        public override async Task UpdateAsync(string currentVersion, string targetVersion)
        {
            await AdResources();
            await base.UpdateAsync(currentVersion, targetVersion);
        }
        public async Task AdResources()
        {
           await _resourceHelper.AddOrUpdateResource(new Dictionary<string, string> 
           {
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.Username"] = "Username",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.Password"] = "Password",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.LoginApiUrl"] = "Login API Url",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.LoginToken"] = "Login Token",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.PurchaseNotifyUrl"] = "Purchase notify url",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.SavedSuccessfully"] = "Saved successfully",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.SavingFailed"] = "Saving failed",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.Configuration"] = "Configuration",
           },
           new Dictionary<string, string> 
           {
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.Username"] = "نام کاربری",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.Password"] = "پسورد",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.LoginApiUrl"] = "آدرس لاگین",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.LoginToken"] = "توکن",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.PurchaseNotifyUrl"] = "آدرس پیام خرید",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.SavedSuccessfully"] = "با موفقیت ذخیره شد",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.SavingFailed"] = "ذخیره نشد",
               ["Finitx.Plugin.Misc.LogEventsDataToServer.Setting.Configuration"] = "تنظیمات",
           });
        }
        public async Task SaveDefaultSetting()
        {
           await _settingService.SaveSettingAsync(new FinitxLogDataSetting
            {
               LoginApiUrl = "https://auth-hub.dpnaco.com/Authenticate/api/login",
               LoginToken="",
               Password="k$J&95PGkNgFPWmu",
               PurchaseNotifyUrl= "https://dpmn.finitx.org/api/purchase/notify",
               Username= "store"
           });
        }
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/FinitxLogDataSetting/Configure";

        }
    }
}
