using Finitx.Common.Helper;
using Finitx.Common.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Infrastructure
{
    public class NopStartup : INopStartup
    {
        public int Order => 3003;

        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFinitxLanguageService, FinitxLanguageService>();
            services.AddScoped<IResourceHelper, ResourceHelper>();
            services.AddScoped<IApiHelper, ApiHelper>();
        }
    }
}
