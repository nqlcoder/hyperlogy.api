using Hyperlogy.BL.Interfaces;
using Hyperlogy.BL.Services;
using Hyperlogy.DL.Interfaces;
using Hyperlogy.DL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.BL.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentServices, StudentServices>();

            services.AddScoped<IStudentRepository, StudentRepository>();

            return services;
        }
    }
}
