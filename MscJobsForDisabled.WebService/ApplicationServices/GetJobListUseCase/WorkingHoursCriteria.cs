using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MscJobsForDisabled.ApplicationServices.GetJobListUseCase 
{
    public class WorkingHoursCriteria : ICriteria<Job>
    {
        public string ExpectedEffects { get; }

        public WorkingHoursCriteria(string expectedEffects)
            => ExpectedEffects = expectedEffects;

        public Expression<Func<Job, bool>> Filter
            => (s => s.WorkingHours == ExpectedEffects);
    }
}
