using AthenaBackend.Application;
using AthenaBackend.Common.DependecyInjection;
using AthenaBackend.Infrastructure;
using AthenaBackend.WebApi.DataApplicationRequest;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;

namespace AthenaBackend.WebApi
{
    public class Startup
    {
        private readonly string DOMAIN_NAME = "AthenaBackend.Domain";
        private readonly string INFRASTRUCTURE_NAME = "AthenaBackend.Infrastructure";
        private readonly string APPLICATION_NAME = "AthenaBackend.Application";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddEntityFrameworkSqlite();


            services.AddGraphQLServer()
                    .AddQueryType<Query>()
                    .AddMutationType<Mutation>();

            services.AddApplication();
            services.AddInfrastructure(Configuration);

            InjectDependencies(services);
        }

        private void InjectDependencies(IServiceCollection services)
        {
            var domainAssembly = GetDomainAssembly();
            var infrastructureAssembly = GetInfrastructureAssembly();
            var applicationAssembly = GetApplicationAssembly();

            services.AddRepositories(domainAssembly, infrastructureAssembly);
            services.AddServices(domainAssembly);
            services.AddReadRepositories(infrastructureAssembly);
            services.AddConverters(domainAssembly);
            services.AddConverters(infrastructureAssembly);
            services.AddConverters(applicationAssembly);
        }

        private Assembly GetApplicationAssembly() => GetAssemblyByName(APPLICATION_NAME);
        private Assembly GetInfrastructureAssembly() => GetAssemblyByName(INFRASTRUCTURE_NAME);
        private Assembly GetDomainAssembly() => GetAssemblyByName(DOMAIN_NAME);
        private Assembly GetAssemblyByName(string assemblyName)
            => AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName.Contains(assemblyName));

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseWriteDbContext<WriteDbContext>(Configuration, serviceProvider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
