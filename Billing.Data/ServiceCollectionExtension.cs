using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Data
{
    //for future additions
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBillingDataServices(this IServiceCollection services)
        {
            //services.AddTransient<IService, Service>();
            return services;
        }
    }
}
