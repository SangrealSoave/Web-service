using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MscJobsForDisabled.DomainObjects.Repositories
{
    public abstract class ReadOnlyJobRepositoryDecorator : IReadOnlyJobRepository
    {
        private readonly IReadOnlyJobRepository _jobRepository;

        public ReadOnlyJobRepositoryDecorator(IReadOnlyJobRepository jobRepository)
        {
            _jobRepository = jobRepository; 
        }

        public virtual async Task<IEnumerable<Job>> GetAllJobs()
        {
            return await _jobRepository?.GetAllJobs();
        }

        public virtual async Task<Job> GetJob(long id)
        {
            return await _jobRepository?.GetJob(id);
        }

        public virtual async Task<IEnumerable<Job>> QueryJobs(ICriteria<Job> criteria)
        {
            return await _jobRepository?.QueryJobs(criteria);
        }
    }
}
