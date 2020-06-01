using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MscJobsForDisabled.ApplicationServices.Repositories;
using MscJobsForDisabled.ApplicationServices.Ports;
using MscJobsForDisabled.ApplicationServices.GetJobListUseCase;
using Xunit;
using MscJobsForDisabled.DomainObjects;

namespace MscJobsForDisabled.WebService.Tests
{
    public class GetJobListUseCaseTest
    {
        private InMemoryJobRepository CreateRoteRepository()
        {
            var repo = new InMemoryJobRepository(new List<Job> {
                new Job { Id = 1, Name = "Имя1", WorkingHours = "Рабочие часы1", Email = "Почта1", Telephone = "Телефон1", Location = "Адрес1"},
                new Job { Id = 2, Name = "Имя2", WorkingHours = "Рабочие часы2", Email = "Почта2", Telephone = "Телефон2", Location = "Адрес2"},
                new Job { Id = 3, Name = "Имя3", WorkingHours = "Рабочие часы3", Email = "Почта3", Telephone = "Телефон3", Location = "Адрес3"},
                new Job { Id = 4, Name = "Имя4", WorkingHours = "Рабочие часы4", Email = "Почта4", Telephone = "Телефон4", Location = "Адрес4"},
            });
            return repo;
        }

        [Fact]
        public void TestGetAllJobs()
        {
            var useCase = new GetJobListUseCase(CreateRoteRepository());
            var outputPort = new OutputPort();

            Assert.True(useCase.Handle(GetJobListUseCaseRequest.CreateAllJobsRequest(), outputPort).Result);
            Assert.Equal<int>(4, outputPort.Jobs.Count());
            Assert.Equal(new long[] { 1, 2, 3, 4 }, outputPort.Jobs.Select(o => o.Id));
        }

        [Fact]
        public void TestGetAllJobsFromEmptyRepository()
        {
            var useCase = new GetJobListUseCase(new InMemoryJobRepository());
            var outputPort = new OutputPort();

            Assert.True(useCase.Handle(GetJobListUseCaseRequest.CreateAllJobsRequest(), outputPort).Result);
            Assert.Empty(outputPort.Jobs);
        }

        [Fact]
        public void TestGetJob()
        {
            var useCase = new GetJobListUseCase(CreateRoteRepository());
            var outputPort = new OutputPort();

            Assert.True(useCase.Handle(GetJobListUseCaseRequest.CreateJobRequest(2), outputPort).Result);
            Assert.Single(outputPort.Jobs, r => 2 == r.Id);
        }

        [Fact]
        public void TestTryGetNotExistingJob()
        {
            var useCase = new GetJobListUseCase(CreateRoteRepository());
            var outputPort = new OutputPort();

            Assert.True(useCase.Handle(GetJobListUseCaseRequest.CreateJobRequest(999), outputPort).Result);
            Assert.Empty(outputPort.Jobs);
        }

    }

    class OutputPort : IOutputPort<GetJobListUseCaseResponse>
    {
        public IEnumerable<Job> Jobs { get; private set; }

        public void Handle(GetJobListUseCaseResponse response)
        {
            Jobs = response.Jobs;
        }
    }

}
