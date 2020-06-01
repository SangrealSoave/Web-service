using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MscJobsForDisabled.ApplicationServices.Repositories
{
    public class InMemoryJobRepository : IReadOnlyJobRepository,
                                           IJobRepository
    {
        private readonly List<Job> _jobs = new List<Job>();

        public InMemoryJobRepository(IEnumerable<Job> jobs = null)
        {
            if (jobs != null)
            {
                _jobs.AddRange(jobs);
            }
        }

        public Task AddJob(Job job)
        {
            _jobs.Add(job);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Job>> GetAllJobs()
        {
            return Task.FromResult(_jobs.AsEnumerable());
        }

        public Task<Job> GetJob(long id)
        {
            return Task.FromResult(_jobs.Where(o => o.Id == id).FirstOrDefault());
        }

        public Task<IEnumerable<Job>> QueryJobs(ICriteria<Job> criteria)
        {
            return Task.FromResult(_jobs.Where(criteria.Filter.Compile()).AsEnumerable());
        }

        public Task RemoveJob(Job job)
        {
            _jobs.Remove(job);
            return Task.CompletedTask;
        }

        public Task UpdateJob(Job job)
        {
            var foundJob = GetJob(job.Id).Result;
            if (foundJob == null)
            {
                AddJob(job);
            }
            else
            {
                if (foundJob != job)
                {
                    _jobs.Remove(foundJob);
                    _jobs.Add(job);
                }
            }
            return Task.CompletedTask;
        }
    }
}
