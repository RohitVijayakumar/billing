using Billing.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Service
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBillingServices(this IServiceCollection services)
        {
            services.AddTransient<IService, Service>();
            return services;
        }
    }
}
