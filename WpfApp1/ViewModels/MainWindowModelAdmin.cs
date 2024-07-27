using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Repositories;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public class MainWindowModelAdmin : ViewModelBase
    {
        private UserAccountModel _currentUserAccount;
        private AdminRepository _adminRepository;
        private string _errorMessage;
        private AdminModule _adminModule;
        private bool _isViewVisible = true;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public AdminModule AdminModule
        {
            get => _adminModule;
            set
            {
                _adminModule = value;
                OnPropertyChanged(nameof(AdminModule));
            }
        }

        public UserAccountModel CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        public ICommand SaveCommand { get; }

        public MainWindowModelAdmin()
        {
            SaveCommand = new ViewModelCommand(ExecuteSave, canExecuteSave);
            _adminRepository = new AdminRepository();
            _adminModule = _adminRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            _currentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private bool canExecuteSave(object obj)
        {
            bool ValiData = true;
            if (string.IsNullOrWhiteSpace(AdminModule.Name))
            {
                ErrorMessage = "* Invalid Name";
                ValiData = false;
            }
            else if (string.IsNullOrWhiteSpace(AdminModule.LastName))
            {
                ErrorMessage = "* Invalid LastName";
                ValiData = false;
            }
            else if (string.IsNullOrWhiteSpace(AdminModule.NumTel))
            {
                ErrorMessage = "* Invalid NumTel";
                ValiData = false;
            }
            else if (string.IsNullOrWhiteSpace(AdminModule.Cin))
            {
                ErrorMessage = "* Invalid Cin";
                ValiData = false;
            }
            else if (string.IsNullOrEmpty(AdminModule.Date.ToString()))
            {
                ErrorMessage = "* Invalid Date";
                ValiData = false;
            }
            else
            {
                ErrorMessage = string.Empty;
            }

            return ValiData;
        }

        private void ExecuteSave(object obj)
        {
            if (canExecuteSave(obj))
            {
                try
                {
                    _adminRepository.UpdateWithoutPasswordUsername(AdminModule);
          
                    CurrentUserAccount.DisplayName = $"{AdminModule.Name} {AdminModule.LastName}";

                    OnPropertyChanged(nameof(CurrentUserAccount));
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error saving admin details: {ex.Message}";
                }
            }
        }

        private void LoadCurrentUserData()
        {
            if (_adminModule != null)
            {
                CurrentUserAccount.Username = AdminModule.Username;
                CurrentUserAccount.DisplayName = $"{AdminModule.Name} {AdminModule.LastName}";
                CurrentUserAccount.ProfilePicture = null; // Update this if you have profile picture logic.
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                OnPropertyChanged(nameof(CurrentUserAccount));
                // Hide child views or display appropriate message.
            }
        }
    }
}

