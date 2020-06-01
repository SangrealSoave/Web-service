using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.ApplicationServices.GetJobListUseCase;
using MscJobsForDisabled.InfrastructureServices.Presenters;

namespace MscJobsForDisabled.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IGetJobListUseCase _getJobListUseCase;

        public JobController(ILogger<JobController> logger, IGetJobListUseCase getJobListUseCase)
        {
            _logger = logger;
            _getJobListUseCase = getJobListUseCase;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllJobs()
        {
            var presenter = new JobListPresenter();
            await _getJobListUseCase.Handle(GetJobListUseCaseRequest.CreateAllJobsRequest(), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("{routeId}")]
        public async Task<ActionResult> GetJob(long JobId)
        {
            var presenter = new JobListPresenter();
            await _getJobListUseCase.Handle(GetJobListUseCaseRequest.CreateJobRequest(JobId), presenter);
            return presenter.ContentResult;
        }
    }
}
