using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Widgets.ProductBadges.Models
{
    public record FinitxProductDataModel : BaseNopEntityModel
    {
        public string Features { get; set; }
        public List<string> FeaturesList { get; set; }
        public bool HideGeneralBlock { get; set; } = false;
        public bool IsComingSoon { get; set; }
    }
}
