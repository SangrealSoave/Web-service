using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.ApplicationServices.Ports.Gateways.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MscJobsForDisabled.InfrastructureServices.Gateways.Database
{
    public class JobEFSqliteGateway : IJobDatabaseGateway
    {
        private readonly JobContext _jobContext;

        public JobEFSqliteGateway(JobContext jobContext)
            => _jobContext = jobContext;

        public async Task<Job> GetJob(long id)
           => await _jobContext.Jobs.Where(r => r.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Job>> GetAllJobs()
            => await _jobContext.Jobs.ToListAsync();

        public async Task<IEnumerable<Job>> QueryJobs(Expression<Func<Job, bool>> filter)
            => await _jobContext.Jobs.Where(filter).ToListAsync();

        public async Task AddJob(Job job)
        {
            _jobContext.Jobs.Add(job);
            await _jobContext.SaveChangesAsync();
        }

        public async Task UpdateJob(Job job)
        {
            _jobContext.Entry(job).State = EntityState.Modified;
            await _jobContext.SaveChangesAsync();
        }

        public async Task RemoveJob(Job job) 
        {
            _jobContext.Jobs.Remove(job);
            await _jobContext.SaveChangesAsync();
        }
    }
}
