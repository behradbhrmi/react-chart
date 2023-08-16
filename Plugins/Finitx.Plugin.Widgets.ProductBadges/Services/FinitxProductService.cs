using Finitx.Common.Constants;
using Finitx.Plugin.Widgets.ProductBadges.Models;
using Nop.Core.Domain.Catalog;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Widgets.ProductBadges.Services
{
    public class FinitxProductService : IFinitxProductService
    {
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
      
        public FinitxProductService(IGenericAttributeService genericAttributeService, ILocalizationService localizationService)
        {
           _genericAttributeService=genericAttributeService;
            _localizationService=localizationService;
        }

        public async Task<List<string>> GetProductFeatures(int productId)
        {
            if (productId <= 0)
                return new List<string>();
          var features= await _genericAttributeService.GetAttributeAsync<string>(new Product { Id = productId }, FinitxCustomerDefaults.FinitxProductFeatures);
            if (string.IsNullOrEmpty(features))
                return new List<string>();
            return features.Split('\n').ToList() ;
        }

        public async Task<bool> GetProductIsComingSoon(int productId)
        {
            if (productId <= 0)
                return false;
            var isComingSoon = await _genericAttributeService.GetAttributeAsync<bool>(new Product { Id = productId }, FinitxCustomerDefaults.FinitxProductIsComingSoon);           
            return isComingSoon;
        }

        public async Task<(string, bool)> SaveProductFeature(FinitxProductDataModel dataModel)
        { 
            //_notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Plugins.Misc.Sendinblue.SMS.Campaigns.Sent"));
            if (dataModel is null)            
                return ((await _localizationService.GetResourceAsync("Finitx.Plugin.Widgets.ProductBadges.ProductData.DataIsNull")), false);
            if (dataModel.Id <=0)
                return ((await _localizationService.GetResourceAsync("Finitx.Plugin.Widgets.ProductBadges.ProductData.ProductIdIsInvalid")), false);
          await  _genericAttributeService.SaveAttributeAsync(new Product { Id = dataModel.Id }, FinitxCustomerDefaults.FinitxProductFeatures, dataModel.Features);
                return ((await _localizationService.GetResourceAsync("Finitx.Plugin.Widgets.ProductBadges.ProductData.SaveSuccess")), true);

        }

        public async Task<(string, bool)> SaveProductIsComingSoon(FinitxProductDataModel dataModel)
        {
            if (dataModel is null)
                return ((await _localizationService.GetResourceAsync("Finitx.Plugin.Widgets.ProductBadges.ProductData.DataIsNull")), false);
            if (dataModel.Id <= 0)
                return ((await _localizationService.GetResourceAsync("Finitx.Plugin.Widgets.ProductBadges.ProductData.ProductIdIsInvalid")), false);
            await _genericAttributeService.SaveAttributeAsync(new Product { Id = dataModel.Id }, FinitxCustomerDefaults.FinitxProductIsComingSoon, dataModel.IsComingSoon);
            return ((await _localizationService.GetResourceAsync("Finitx.Plugin.Widgets.ProductBadges.ProductData.SaveSuccess")), true);
        }
    }
}
