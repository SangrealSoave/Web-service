using System.Net;
using Newtonsoft.Json;
using MscJobsForDisabled.ApplicationServices.Ports;
using MscJobsForDisabled.ApplicationServices.GetJobListUseCase;

namespace MscJobsForDisabled.InfrastructureServices.Presenters
{
    public class JobListPresenter : IOutputPort<GetJobListUseCaseResponse>
    {
        public JsonContentResult ContentResult { get; }

        public JobListPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GetJobListUseCaseResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            ContentResult.Content = response.Success ? JsonConvert.SerializeObject(response.Jobs) : JsonConvert.SerializeObject(response.Message);
        }
    }
}
