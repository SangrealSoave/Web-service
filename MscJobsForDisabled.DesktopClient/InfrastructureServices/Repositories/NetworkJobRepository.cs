using MscJobsForDisabled.ApplicationServices.Ports.Cache;
using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MscJobsForDisabled.InfrastructureServices.Repositories
{
    public class NetworkJobRepository : NetworkRepositoryBase, IReadOnlyJobRepository
    {
        private readonly IDomainObjectsCache<Job> _jobCache;

        public NetworkJobRepository(string host, ushort port, bool useTls, IDomainObjectsCache<Job> jobCache)
            : base(host, port, useTls)
            => _jobCache = jobCache;

        public async Task<Job> GetJob(long id)
            => CacheAndReturn(await ExecuteHttpRequest<Job>($"Job/{id}"));

        public async Task<IEnumerable<Job>> GetAllJobs()
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<Job>>($"Job"), allObjects: true);

        public async Task<IEnumerable<Job>> QueryJobs(ICriteria<Job> criteria)
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<Job>>($"Job"), allObjects: true)
               .Where(criteria.Filter.Compile());

        private IEnumerable<Job> CacheAndReturn(IEnumerable<Job> jobs, bool allObjects = false)
        {
            if (allObjects)
            {
                _jobCache.ClearCache();
            }
            _jobCache.UpdateObjects(jobs, DateTime.Now.AddDays(1), allObjects);
            return jobs;
        }

        private Job CacheAndReturn(Job job)
        {
            _jobCache.UpdateObject(job, DateTime.Now.AddDays(1));
            return job;
        }
    }
}
