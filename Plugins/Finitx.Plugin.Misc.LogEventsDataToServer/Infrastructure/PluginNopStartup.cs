using Finitx.Plugin.Misc.LogEventsDataToServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Infrastructure
{
    public class PluginNopStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            
           services.AddScoped<IFinitxUserService, FinitxUserService>();
           services.AddScoped<IFinitxOrderService, FinitxOrderService>();
        }

        public void Configure(IApplicationBuilder application)
        {

        }

        public int Order => 11;
    }
}