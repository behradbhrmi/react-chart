using Finitx.Common.Helper;
using Finitx.Common.Service;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finitx.Plugin.Widgets.ProductBadges
{
    /// <summary>
    /// Rename this file and change to the correct type
    /// </summary>
    public class ProductBadgesPlugin : BasePlugin, IWidgetPlugin
    {
        //public PluginDescriptor PluginDescriptor { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public bool HideInWidgetList => false;
        private readonly ILocalizationService _localizationService;
        private readonly IFinitxLanguageService _finitxLanguageService;
        private readonly IResourceHelper _resourceHelper;
        public ProductBadgesPlugin(
            ILocalizationService localizationService,
            IFinitxLanguageService finitxLanguageService,
            IResourceHelper resourceHelper)
        {
            _localizationService = localizationService;
            _finitxLanguageService = finitxLanguageService;
            _resourceHelper = resourceHelper;
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            //if (widgetZone == PublicWidgetZones.ProductBoxAddinfoMiddle)
            //    return ProductBadgesDefaults.MIDDLE_BADGE_VIEW_COMPONENT_NAME;

            if (widgetZone == AdminWidgetZones.ProductDetailsBlock)
                return ProductBadgesDefaults.FINITX_Product_Data_VIEW_COMPONENT_NAME;
            
            if (widgetZone == PublicWidgetZones.ProductBoxAddinfoBefore)
                return ProductBadgesDefaults.BEFORE_BADGE_VIEW_COMPONENT_NAME;

            return string.Empty;
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { /*PublicWidgetZones.ProductBoxAddinfoMiddle,*/ PublicWidgetZones.ProductBoxAddinfoBefore, AdminWidgetZones.ProductDetailsBlock });

            //return new List<string> { PublicWidgetZones.ProductBoxAddinfoMiddle };
        }

        public override async Task InstallAsync()
        {
            await AddOrUpdateResources();
            await base.InstallAsync();
        }
        public override async Task UpdateAsync(string currentVersion, string targetVersion)
        {
            await AddOrUpdateResources();
            await base.UpdateAsync(currentVersion, targetVersion);
        }
        private async Task AddOrUpdateResources()
        {
            var enResource = new Dictionary<string, string>
            {
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.PartLabele"] = "Features",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.Description"] = "Seprate Feature By Enter!",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.SaveFeature"] = "Save",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.SaveSuccess"] = "Features successfully saved",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.SavingFailed"] = "Saving features failed",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.DataIsNull"] = "Data is null",               
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.ProductIdIsInvalid"] = "Product Id Is Invalid",
            };
            var faResource = new Dictionary<string, string>
            {
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.PartLabele"] = ",ویژگی‌ها",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.Description"] = "ویژگی‌ها را با اینتر جدا کنید!",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.SaveFeature"] = "ذخیره",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.SaveSuccess"] = "ویژگی‌ها با موفقیت ذخیره شد ",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.SavingFailed"] = "ذخیره ویژگی‌ها شکست خورد",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.DataIsNull"] = "اطلاعات خالی است",
                ["Finitx.Plugin.Widgets.ProductBadges.ProductData.ProductIdIsInvalid"] = "شناسه محصول نامعتبر است",
            };
            await _resourceHelper.AddOrUpdateResource(enResource, faResource);
        }

    }
}
