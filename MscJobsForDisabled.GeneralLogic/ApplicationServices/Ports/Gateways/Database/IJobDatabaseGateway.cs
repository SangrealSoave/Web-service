using MscJobsForDisabled.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MscJobsForDisabled.ApplicationServices.Ports.Gateways.Database 
{
    public interface IJobDatabaseGateway
    {
        Task AddJob(Job job);

        Task RemoveJob(Job job);

        Task UpdateJob(Job job);

        Task<Job> GetJob(long id);

        Task<IEnumerable<Job>> GetAllJobs();

        Task<IEnumerable<Job>> QueryJobs(Expression<Func<Job, bool>> filter);

    }
}
