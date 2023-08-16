using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Infrastructure
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //set route of congif page 
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/Plugins/Finitx.Plugin.Misc.LogEventsDataToServer/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Finitx.Plugin.Misc.LogEventsDataToServer/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            //change route of customer controller
            if (context.AreaName == null && context.ControllerName == "FinitxCustomer")
            {
                viewLocations = new string[] { $"/Views/Customer/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            return viewLocations;
        }
    }
}
