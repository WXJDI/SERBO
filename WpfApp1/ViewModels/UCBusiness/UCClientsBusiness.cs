using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp1.Views.DataEntry;

namespace WpfApp1.ViewModels.UCBusiness
{
    public class UCClientsBusiness : INotifyPropertyChanged
    {
        public bool ToSave { get; set; }
        private string _searchName;
        private ObservableCollection<ClientModel> _listOfClients;
        private ObservableCollection<ClientModel> _filteredClients;
        private ClientRepository _clientRepository;
        private ClientModel _selectedClient;

        public UCClientsBusiness()
        {
            _clientRepository = new ClientRepository();
            _listOfClients = new ObservableCollection<ClientModel>(_clientRepository.GetByAll());
            _filteredClients = new ObservableCollection<ClientModel>(_listOfClients);
            AddCommand = new ViewModelCommand(AddProduct, CanExecuteCommand);
            EditCommand = new ViewModelCommand(EditProduct, CanExecuteCommand);
            DeleteCommand = new ViewModelCommand(DeleteProduct, CanExecuteCommand);
            SaveCommand = new ViewModelCommand(SaveProduct, CanExecuteCommand);
        }

        public string SearchName
        {
            get => _searchName;
            set
            {
                _searchName = value;
                OnPropertyChanged(nameof(SearchName));
                FilterProducts();
            }
        }

        public ObservableCollection<ClientModel> ListOfClients
        {
            get => _filteredClients;
            set
            {
                _filteredClients = value;
                OnPropertyChanged(nameof(ListOfClients));
            }
        }

        public ClientModel SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        Views.DataEntry.ClientDataEntry clientDataEntry;
        private void FilterProducts()
        {
            if (string.IsNullOrEmpty(_searchName))
            {
                ListOfClients = new ObservableCollection<ClientModel>(_clientRepository.GetByAll());
            }
            else
            {
                ListOfClients = new ObservableCollection<ClientModel>(
                    _clientRepository.GetByAll().Where(p => p.Name.ToLower().Contains(_searchName.ToLower())));
            }
        }
        private void AddProduct(object obj)
        {
            ToSave = true;
            _selectedClient = new ClientModel();

            clientDataEntry = new ClientDataEntry();
            clientDataEntry.DataContext = this;

            clientDataEntry.ShowDialog();
        }

        private void EditProduct(object obj)
        {
            if (_selectedClient != null)
            {
                ToSave = false;

                clientDataEntry = new ClientDataEntry();
                clientDataEntry.DataContext = this;

                clientDataEntry.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a product!");
            }
        }

        private void DeleteProduct(object obj)
        {
            if (_selectedClient != null)
            {
                if (!(_clientRepository.HasOrders(_selectedClient.IdClient)))
                {
                    _clientRepository.Delete(_selectedClient);
                    RefreshProductList();
                }
                else
                {
                    System.Windows.MessageBox.Show("CLIENT can't be deleted !! Please Delete Orders Associated with this Client !!!");
                }
                _clientRepository.Delete(_selectedClient);
                RefreshProductList();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a product!");
            }
        }

        private void SaveProduct(object obj)
        {
            if (ToSave)
            {
                _clientRepository.Add(_selectedClient);
            }
            else
            {
                _clientRepository.Update(_selectedClient);
            }

            clientDataEntry.Close();

            RefreshProductList();
        }

        private void RefreshProductList()
        {
            var clients = _clientRepository.GetByAll();
            ListOfClients = new ObservableCollection<ClientModel>(clients);
        }

        private bool CanExecuteCommand(object obj)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
