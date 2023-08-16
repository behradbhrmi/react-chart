using Finitx.Common.Helper;
using Finitx.Common.Service;
using Finitx.Plugin.Widgets.ProductBadges.Factories;
using Finitx.Plugin.Widgets.ProductBadges.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Web.Factories;

namespace Finitx.Plugin.Widgets.ProductBadges.Infrastructure
{
    public class PluginNopStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
      {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            //register services and interfaces
            services.AddScoped<IFinitxProductService, FinitxProductService>();
          

        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 10000;
    }
}