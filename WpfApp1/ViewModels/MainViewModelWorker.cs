using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Repositories;

namespace WpfApp1.ViewModels
{
    public class MainViewModelWorker : ViewModelBase
    {
        private UserAccountModel _currentUserAccount;
        
        private IWorkerRepository workerRepository;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public MainViewModelWorker()
        {
            workerRepository = new WorkerRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            WorkerModel worker = workerRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (worker != null)
            {
                CurrentUserAccount.Username = worker.Username;
                CurrentUserAccount.DisplayName = $" {worker.Name} {worker.LastName}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                //Hide child views.
            }
        }
    }
}
