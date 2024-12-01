using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMon.Application
{
    public static class DependencyInjection
    {
        // eventuell später MediatR pattern Service hier einbauen.

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }

    }
}
