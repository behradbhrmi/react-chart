using Finitx.Plugin.Widgets.ProductBadges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Widgets.ProductBadges.Services
{
    public interface IFinitxProductService
    {
        Task<(string, bool)> SaveProductFeature(FinitxProductDataModel dataModel);
        Task<(string, bool)> SaveProductIsComingSoon(FinitxProductDataModel dataModel);
        Task<List<string>> GetProductFeatures(int productId);
        Task<bool> GetProductIsComingSoon(int productId);
    }
}
