using MscJobsForDisabled.ApplicationServices.Ports.Cache;
using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.DomainObjects.Ports;
using MscJobsForDisabled.DomainObjects.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MscJobsForDisabled.InfrastructureServices.Repositories
{
    public class CachedReadOnlyJobRepository : ReadOnlyJobRepositoryDecorator
    {
        private readonly IDomainObjectsCache<Job> _jobsCache;

        public CachedReadOnlyJobRepository(IReadOnlyJobRepository jobRepository,
                                             IDomainObjectsCache<Job> jobsCache)
            : base(jobRepository)
            => _jobsCache = jobsCache;

        public async override Task<Job> GetJob(long id)
            => _jobsCache.GetObject(id) ?? await base.GetJob(id);

        public async override Task<IEnumerable<Job>> GetAllJobs()
            => _jobsCache.GetObjects() ?? await base.GetAllJobs();

        public async override Task<IEnumerable<Job>> QueryJobs(ICriteria<Job> criteria)
            => _jobsCache.GetObjects()?.Where(criteria.Filter.Compile()) ?? await base.QueryJobs(criteria);

    }
}
