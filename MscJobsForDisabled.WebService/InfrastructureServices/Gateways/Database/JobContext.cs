using Microsoft.EntityFrameworkCore;
using MscJobsForDisabled.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MscJobsForDisabled.InfrastructureServices.Gateways.Database
{
    public class JobContext : DbContext 
    {
        public DbSet<Job> Jobs { get; set; }

        public JobContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FillTestData(modelBuilder);
        }
        private void FillTestData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().HasData(
                new
                {
                    Id = 1L,
                    Name = "Инженер-программист",
                    WorkingHours = "пятидневная рабочая неделя ",
                    Email = "arina.donich@nami.ru  ",
                    Location = "город Москва, Автомоторная улица, дом 2",
                    Telephone = "(495) 456-43-51"
                },
                new
                {
                    Id = 2L,
                    Name = "Инженер-конструктор",
                    WorkingHours = "пятидневная рабочая неделя ",
                    Email = "arina.donich@nami.ru  ",
                    Location = "город Москва, Автомоторная улица, дом 2",
                    Telephone = "(495) 456-43-51"
                },
                 new
                 {
                     Id = 3L,
                     Name = "Продавец-консультант",
                     WorkingHours = "график сменности  ",
                     Email = "tishumilina@rolf.ru",
                     Location = "город Москва, 39-й километр Московской Кольцевой Автодороги, владение 1, строение 1",
                     Telephone = "(495) 777-77-15 доб. 30171"
                 },
                 new
                 {
                     Id = 4L,
                     Name = "Переводчик",
                     WorkingHours = "с неполным рабочим днем",
                     Email = "MGorbacheva@nestro.ru ",
                     Location = "город Москва, Армянский переулок, дом 9, строение 1 ",
                     Telephone = "(495) 748-64-24 доб. 1008 "
                 }
            );
        }
    }
}
