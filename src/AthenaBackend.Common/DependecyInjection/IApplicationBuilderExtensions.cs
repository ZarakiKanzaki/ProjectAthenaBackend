using AthenaBackend.Common.DependecyInjection.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace AthenaBackend.Common.DependecyInjection
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseWriteDbContext<T>(this IApplicationBuilder thisApplicationBuilder,
                                                                        IConfiguration configuration,
                                                                        IServiceProvider serviceProvider)
            where T : DbContext
        {
            thisApplicationBuilder.UseMiddleware<UseWriteDbContextMiddleware<T>>();

            return thisApplicationBuilder;
        }
    }
}
