using Finitx.Common.Service;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Helper
{
    public class ResourceHelper : IResourceHelper
    {
        private readonly IFinitxLanguageService _finitxLanguageService;
        private readonly ILocalizationService _localizationService;
        public ResourceHelper(
          IFinitxLanguageService finitxLanguageService,
          ILocalizationService localizationService  
            )
        {
            _finitxLanguageService = finitxLanguageService;
            _localizationService = localizationService;
        }
        public async Task AddOrUpdateResource(Dictionary<string, string> enResource, Dictionary<string, string> faResource)
        {
            var enLangId = await _finitxLanguageService.GetLanguageByUniqueSeoCode("en");
            if (enLangId is null)            
                enLangId = await _finitxLanguageService.AddAmericanEnglishLanguage();
            foreach (var item in enResource)
            {
  await _localizationService.AddOrUpdateLocaleResourceAsync(item.Key,item.Value, "en");
            }
          

            var faLangId = await _finitxLanguageService.GetLanguageByUniqueSeoCode("fa");
            if (faLangId is null)            
                faLangId = await _finitxLanguageService.AddPresianLanguage();
            foreach (var item in faResource)
            {
                await _localizationService.AddOrUpdateLocaleResourceAsync(item.Key, item.Value, "fa");
            }
           
        }
    }
}
