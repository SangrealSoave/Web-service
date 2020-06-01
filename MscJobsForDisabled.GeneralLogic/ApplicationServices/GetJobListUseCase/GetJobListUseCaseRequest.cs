using MscJobsForDisabled.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MscJobsForDisabled.ApplicationServices.GetJobListUseCase 
{
    public class GetJobListUseCaseRequest : IUseCaseRequest<GetJobListUseCaseResponse>
    {
        public long? JobId { get; private set; }

        private GetJobListUseCaseRequest()
        { }

        public static GetJobListUseCaseRequest CreateAllJobsRequest()
        {
            return new GetJobListUseCaseRequest();
        }

        public static GetJobListUseCaseRequest CreateJobRequest(long jobId)
        {
            return new GetJobListUseCaseRequest() { JobId = jobId };

        }
    }
}

