using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MscJobsForDisabled.ApplicationServices.Interfaces;

namespace MscJobsForDisabled.ApplicationServices.GetJobListUseCase
{
    public interface IGetJobListUseCase : IUseCase<GetJobListUseCaseRequest, GetJobListUseCaseResponse>
    {
    }
}
