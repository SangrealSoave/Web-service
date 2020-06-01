using MscJobsForDisabled.ApplicationServices.GetJobListUseCase;
using MscJobsForDisabled.ApplicationServices.Ports;
using MscJobsForDisabled.DomainObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MscJobsForDisabled.DesktopClient.InfrastructureServices.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IGetJobListUseCase _getJobListUseCase;

        public MainViewModel(IGetJobListUseCase getJobListUseCase)
            => _getJobListUseCase = getJobListUseCase;

        private Task<bool> _loadingTask;
        private Job _currentJob;
        private ObservableCollection<Job> _jobs;

        public event PropertyChangedEventHandler PropertyChanged;

        public Job CurrentJob
        {
            get => _currentJob;
            set
            {
                if (_currentJob != value)
                {
                    _currentJob = value;
                    OnPropertyChanged(nameof(CurrentJob));
                }
            }
        }

        private async Task<bool> LoadJobs()
        {
            var outputPort = new OutputPort();
            bool result = await _getJobListUseCase.Handle(GetJobListUseCaseRequest.CreateAllJobsRequest(), outputPort);
            if (result)
            {
                Jobs = new ObservableCollection<Job>(outputPort.Jobs);
            }
            return result;
        }

        public ObservableCollection<Job> Jobs
        {
            get
            {
                if (_loadingTask == null)
                {
                    _loadingTask = LoadJobs();
                }

                return _jobs;
            }
            set
            {
                if (_jobs != value)
                {
                    _jobs = value;
                    OnPropertyChanged(nameof(Jobs));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private class OutputPort : IOutputPort<GetJobListUseCaseResponse>
        {
            public IEnumerable<Job> Jobs { get; private set; }

            public void Handle(GetJobListUseCaseResponse response)
            {
                if (response.Success)
                {
                    Jobs = new ObservableCollection<Job>(response.Jobs);
                }
            }
        }
    }
}
