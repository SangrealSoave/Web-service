using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.DomainObjects.Ports;
using MscJobsForDisabled.ApplicationServices.GetJobListUseCase;
using MscJobsForDisabled.ApplicationServices.Repositories;
using System.Collections.Generic;

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
            services.AddScoped<InMemoryJobRepository>(x => new InMemoryJobRepository(
               new List<Job>()
               {
                    new Job()
                    {
                    Id = 1,
                    Name = "Инженер-программист",
                    WorkingHours = "пятидневная рабочая неделя ",
                    Email = "arina.donich@nami.ru  ",
                    Location = "город Москва, Автомоторная улица, дом 2",
                    Telephone = "(495) 456-43-51"
                    },
                    new Job()
                    {
                    Id = 2,
                    Name = "Инженер-конструктор",
                    WorkingHours = "пятидневная рабочая неделя ",
                    Email = "arina.donich@nami.ru  ",
                    Location = "город Москва, Автомоторная улица, дом 2",
                    Telephone = "(495) 456-43-51"
                    },
                    new Job
                    {
                    Id = 3,
                    Name = "Продавец-консультант",
                    WorkingHours = "график сменности  ",
                    Email = "tishumilina@rolf.ru",
                    Location = "город Москва, 39-й километр Московской Кольцевой Автодороги, владение 1, строение 1",
                    Telephone = "(495) 777-77-15 доб. 30171"
                    }
            }));
            services.AddScoped<IReadOnlyJobRepository>(x => x.GetRequiredService<InMemoryJobRepository>());
            services.AddScoped<IJobRepository>(x => x.GetRequiredService<InMemoryJobRepository>());

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
