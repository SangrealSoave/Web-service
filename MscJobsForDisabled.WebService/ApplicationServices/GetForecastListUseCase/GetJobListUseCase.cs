using System.Threading.Tasks;
using System.Collections.Generic;
using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.DomainObjects.Ports;
using MscJobsForDisabled.ApplicationServices.Ports;

namespace MscJobsForDisabled.ApplicationServices.GetJobListUseCase 
{
    public class GetJobListUseCase : IGetJobListUseCase
    {
        private readonly IReadOnlyJobRepository _readOnlyJobRepository;

        public GetJobListUseCase(IReadOnlyJobRepository readOnlyJobRepository)
            => _readOnlyJobRepository = readOnlyJobRepository;

        public async Task<bool> Handle(GetJobListUseCaseRequest request, IOutputPort<GetJobListUseCaseResponse> outputPort)
        {
            IEnumerable<Job> jobs = null;
            if (request.JobId != null)
            {
                var job = await _readOnlyJobRepository.GetJob(request.JobId.Value);
                jobs = (job != null) ? new List<Job>() { job } : new List<Job>();

            }
            else
            {
                jobs = await _readOnlyJobRepository.GetAllJobs();
            }
            outputPort.Handle(new GetJobListUseCaseResponse(jobs));
            return true;
        }
    }
}
