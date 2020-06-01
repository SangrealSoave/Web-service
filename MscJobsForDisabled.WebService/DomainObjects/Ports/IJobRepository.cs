using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MscJobsForDisabled.DomainObjects.Ports
{
    public interface IReadOnlyJobRepository
    {
        Task<Job> GetJob(long id);

        Task<IEnumerable<Job>> GetAllJobs();

        Task<IEnumerable<Job>> QueryJobs(ICriteria<Job> criteria);

    }

    public interface IJobRepository
    {
        Task AddJob(Job job);

        Task RemoveJob(Job job);

        Task UpdateJob(Job job);
    }
}
