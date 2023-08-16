using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Services;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Settings;
using Refit;
using System;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="appSettings">App settings</param>
        public void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {

            //services.AddScoped<ICarawayPaymentService,CarawayPaymentService>();
            services.AddHttpClient<ICarawayPaymentService, CarawayPaymentService>().SetHandlerLifetime(TimeSpan.FromMinutes(5));
            //Set lifetime to five minutes
            //        services
            //.AddRefitClient<IGitHubApi>()
            //.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.github.com"));
        }


        public int Order => 1;
    }
}
