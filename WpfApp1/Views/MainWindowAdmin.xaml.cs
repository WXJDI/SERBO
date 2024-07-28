using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindowAdmin.xaml
    /// </summary>
    public partial class MainWindowAdmin : Window
    {
        public MainWindowAdmin()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BtnScreen_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;

                WindowStyle = WindowStyle.None;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Views.UCs.UCWorkers wrkersControle = new UCs.UCWorkers();
            grcontent.Children.Add(wrkersControle);

        }
        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            Views.UCs.UCClients clientscontrole = new UCs.UCClients();
            
            grcontent.Children.Add(clientscontrole);

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Views.UCs.UCStock stockcontrole = new UCs.UCStock();

            grcontent.Children.Add(stockcontrole);
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            Views.UCs.UCOrders Ordercontrole = new UCs.UCOrders();

            grcontent.Children.Add(Ordercontrole);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
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
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Views.UserProfileEdit.AdminProfileEdit adminProfileEdit = new Views.UserProfileEdit.AdminProfileEdit();

            grcontent.Children.Add(adminProfileEdit);
        }
        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            Views.UserProfileEdit.ResetPassword resetPassword = new Views.UserProfileEdit.ResetPassword();

            grcontent.Children.Add(resetPassword);
        }
    }
}
