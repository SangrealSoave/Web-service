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
                new Job { Id = 1, Name = "���1", WorkingHours = "������� ����1", Email = "�����1", Telephone = "�������1", Location = "�����1"},
                new Job { Id = 2, Name = "���2", WorkingHours = "������� ����2", Email = "�����2", Telephone = "�������2", Location = "�����2"},
                new Job { Id = 3, Name = "���3", WorkingHours = "������� ����3", Email = "�����3", Telephone = "�������3", Location = "�����3"},
                new Job { Id = 4, Name = "���4", WorkingHours = "������� ����4", Email = "�����4", Telephone = "�������4", Location = "�����4"},
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
