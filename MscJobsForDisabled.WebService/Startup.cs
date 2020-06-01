using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MscJobsForDisabled.DomainObjects.Ports;
using MscJobsForDisabled.ApplicationServices.GetJobListUseCase;
using MscJobsForDisabled.ApplicationServices.Ports.Gateways.Database;
using MscJobsForDisabled.InfrastructureServices.Gateways.Database;
using Microsoft.EntityFrameworkCore;
using MscJobsForDisabled.ApplicationServices.Repositories;

namespace MscJobsForDisabled.WebService 
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<JobContext>(opts =>
                            opts.UseSqlite($"Filename={System.IO.Path.Combine(System.Environment.CurrentDirectory, "MscJobsForDisabled.db")}")
                        );

            services.AddScoped<IJobDatabaseGateway, JobEFSqliteGateway>();

            services.AddScoped<DbJobRepository>();
            services.AddScoped<IReadOnlyJobRepository>(x => x.GetRequiredService<DbJobRepository>());
            services.AddScoped<IJobRepository>(x => x.GetRequiredService<DbJobRepository>());

            services.AddScoped<IGetJobListUseCase, GetJobListUseCase>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
