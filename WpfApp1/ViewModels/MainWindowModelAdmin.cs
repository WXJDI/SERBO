using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Repositories;

namespace WpfApp1.ViewModels
{
    public class MainWindowModelAdmin : ViewModelBase
    {
        private UserAccountModel _currentUserAccount;

        private IUserRepository userRepository;



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

        public MainWindowModelAdmin()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private bool canExecuteWorkersCommand(object obj)
        {
            return true;
        }


        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $" {user.Name} {user.LastName}";
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
