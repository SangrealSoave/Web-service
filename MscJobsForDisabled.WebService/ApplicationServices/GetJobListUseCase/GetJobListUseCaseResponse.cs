using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MscJobsForDisabled.ApplicationServices.GetJobListUseCase 
{
    public class GetJobListUseCaseResponse : UseCaseResponse
    {
        public IEnumerable<Job> Jobs { get; }

        public GetJobListUseCaseResponse(IEnumerable<Job> jobs) => Jobs = jobs;
    }
}
