using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MscJobsForDisabled.DomainObjects;

namespace MscJobsForDisabled.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;

        public JobController(ILogger<JobController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Job> GetJob()
        {
            return new List<Job>() 
            { 
                new Job() 
                { 
                    Id = 1, 
                    Name = "Техник",                   
                    Location ="двенадцатичасовой прогноз",
                    Specialization = "Отдел камеральных работ",
                    WorkingHours = "с неполным рабочим днем", 
                    Phone = "(495) 950-86-89",
                    Email = "BukhantsevAB@gtp.transneft.ru"
                } 
            };
        }
    }
}
