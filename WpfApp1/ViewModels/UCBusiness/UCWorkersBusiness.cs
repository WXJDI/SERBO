using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Repositories;
using WpfApp1.Views.DataEntry;

namespace WpfApp1.ViewModels.UCBusiness
{
    public class UCWorkersBusiness : INotifyPropertyChanged
    {
        public bool ToSave { get; set; }
        private string _searchName;
        private ObservableCollection<WorkerModel> _listOfWorkers;
        private ObservableCollection<WorkerModel> _filteredWorkers;
        private WorkerRepository _workerRepository;
        private WorkerModel _selectedworker;

        public UCWorkersBusiness()
        {
            _workerRepository = new WorkerRepository();
            _listOfWorkers = new ObservableCollection<WorkerModel>(_workerRepository.GetByAll());
            _filteredWorkers = new ObservableCollection<WorkerModel>(_listOfWorkers);
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

        public ObservableCollection<WorkerModel> ListOfWorkers
        {
            get => _filteredWorkers;
            set
            {
                _filteredWorkers = value;
                OnPropertyChanged(nameof(ListOfWorkers));
            }
        }

        public WorkerModel SelectedWorker
        {
            get => _selectedworker;
            set
            {
                _selectedworker = value;
                OnPropertyChanged(nameof(SelectedWorker));
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private void FilterProducts()
        {
            if (string.IsNullOrEmpty(_searchName))
            {
                ListOfWorkers = new ObservableCollection<WorkerModel>(_workerRepository.GetByAll());
            }
            else
            {
                ListOfWorkers = new ObservableCollection<WorkerModel>(
                    _workerRepository.GetByAll().Where(p => p.Name.ToLower().Contains(_searchName.ToLower())));
            }
        }
        Views.DataEntry.WorkerDataEntry productDataEntry;
        private void AddProduct(object obj)
        {
            ToSave = true;
            _selectedworker = new WorkerModel();

            productDataEntry = new WorkerDataEntry();
            productDataEntry.DataContext = this;

            productDataEntry.ShowDialog();
        }

        private void EditProduct(object obj)
        {
            if (_selectedworker != null)
            {
                ToSave = false;

                productDataEntry = new WorkerDataEntry();
                productDataEntry.DataContext = this;

                productDataEntry.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a product!");
            }
        }

        private void DeleteProduct(object obj)
        {
            if (_selectedworker != null)
            {
                _workerRepository.Delete(_selectedworker);
                RefreshProductList();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a product!");
            }
        }

        private void SaveProduct(object obj)
        {
            if (_workerRepository.GetByUsername(_selectedworker.Username)==null)
            {
                if (ToSave)
                {
                    _workerRepository.Add(_selectedworker);
                }
                else
                {
                    _workerRepository.Update(_selectedworker);
                }
                productDataEntry.Close();

                RefreshProductList();
            }
            else
                System.Windows.MessageBox.Show("Invalid Username");
            
        }

        private void RefreshProductList()
        {
            var products = _workerRepository.GetByAll();
            ListOfWorkers = new ObservableCollection<WorkerModel>(products);
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

