using System;
using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.DomainObjects.Ports;
using MscJobsForDisabled.ApplicationServices.Ports.Gateways.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MscJobsForDisabled.ApplicationServices.Repositories
{
    public class DbJobRepository : IReadOnlyJobRepository,
                                     IJobRepository
    {
        private readonly IJobDatabaseGateway _databaseGateway;

        public DbJobRepository(IJobDatabaseGateway databaseGateway)
            => _databaseGateway = databaseGateway;

        public async Task<Job> GetJob(long id)
            => await _databaseGateway.GetJob(id);

        public async Task<IEnumerable<Job>> GetAllJobs()
            => await _databaseGateway.GetAllJobs();

        public async Task<IEnumerable<Job>> QueryJobs(ICriteria<Job> criteria)
            => await _databaseGateway.QueryJobs(criteria.Filter);

        public async Task AddJob(Job job)
            => await _databaseGateway.AddJob(job);

        public async Task RemoveJob(Job job)
            => await _databaseGateway.RemoveJob(job);

        public async Task UpdateJob(Job job)
            => await _databaseGateway.UpdateJob(job);
    }
}
