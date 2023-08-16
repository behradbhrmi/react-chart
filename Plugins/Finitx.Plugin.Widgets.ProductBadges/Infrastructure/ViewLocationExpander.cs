using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Finitx.Plugin.Widgets.ProductBadges.Infrastructure
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/Plugins/Finitx.Plugin.Widgets.ProductBadges/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Finitx.Plugin.Widgets.ProductBadges/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }           
            if (context.ViewName == "_ProductBox")
            {
                viewLocations = new[] { $"/Plugins/Finitx.Plugin.Widgets.ProductBadges/Views/Shared/_ProductBox.cshtml" }.Concat(viewLocations);
            } 
            //feature of product
            if (context.ViewName == "_ProductAttributes")
            {
                viewLocations = new[] { $"/Plugins/Finitx.Plugin.Widgets.ProductBadges/Views/Home/_ProductAttributes.cshtml" }.Concat(viewLocations);
            }
            //set custom compare product as orginal CompareProducts not used for now
            //if (context.ViewName == "_CompareProducts")
            //{
            //    viewLocations = new[] { $"/Plugins/Finitx.Plugin.Widgets.ProductBadges/Views/Shared/_CompareProducts.cshtml" }.Concat(viewLocations);
            //}
            //change home page
            if (context.ViewName == "Index"&&context.ControllerName=="Home"&&context.AreaName!="Admin")
            {
                viewLocations = new[] { $"/Plugins/Finitx.Plugin.Widgets.ProductBadges/Views/Home/index.cshtml" }.Concat(viewLocations);
            }
            //plan page
            if (context.ViewName == "Components/HomepageProducts/Default")
            {
                viewLocations = new[] { $"/Plugins/Finitx.Plugin.Widgets.ProductBadges/Views/Shared/HomepageProducts/Default.cshtml" }.Concat(viewLocations);
            }
            return viewLocations;
        }
    }
}
