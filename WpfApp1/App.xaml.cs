using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Repositories;
using WpfApp1.ViewModels;
using WpfApp1.Views;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
                AdminRepository context = new AdminRepository();
                if (context.GetAdmin() == null)
                {
                    AdminModule admin = new AdminModule()
                    {
                        IdWorker = 0,
                        Name = "Admin",
                        LastName = "Admin",
                        Cin = "00000000",
                        Email = "admin@com",
                        NumTel = "0000000000",
                        Password = "admin123",
                        Username = "admin"
                    };
                    context.Add(admin);
                }
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var loginViewModel = (LoginViewModel)loginView.DataContext;
                    Window mainView;
                    if (loginViewModel.UserRole == "Admin")
                    {
                        mainView = new MainWindowAdmin();
                    }
                    else
                    {
                        mainView = new MainWindowWorker();
                    }
                    mainView.Show();
                    loginView.Close();
                }
            };
        }
    }
}
