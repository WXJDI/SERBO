﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Repositories;
using WpfApp1.Views.DataEntry;

namespace WpfApp1.ViewModels.UCBusiness
{
    public class UCOrdersBusiness : INotifyPropertyChanged
    {
        public bool ToSave { get; set; }
        public bool ToSave1 { get; set; }
        private string _searchName;
        private ObservableCollection<OrderModel> _listOfOrders;
        private ObservableCollection<ProductModel> _listOfProducts;
        private ObservableCollection<OrderModel> _filteredOrders;
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;
        private OrderModel _selectedOrder;
        private ProductModel _selectedProduct;

        public UCOrdersBusiness()
        {
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
            _listOfOrders = new ObservableCollection<OrderModel>(_orderRepository.GetByAll());
            _listOfProducts = new ObservableCollection<ProductModel>();
            _filteredOrders = new ObservableCollection<OrderModel>(_listOfOrders);
            AddCommand = new ViewModelCommand(AddOrder, CanExecuteCommand);
            EditCommand = new ViewModelCommand(EditProduct, CanExecuteCommand);
            DeleteCommand = new ViewModelCommand(DeleteProduct, CanExecuteCommand);
            AddProductCommand = new ViewModelCommand(AddProductOrder, CanExecuteProductCommand);
            DeleteProductCommand = new ViewModelCommand(DeleteProductProduct, CanExecuteProductCommand);
            SaveCommand = new ViewModelCommand(SaveProduct, CanExecuteCommand);
            SaveCommand1 = new ViewModelCommand(Save1Product, Can1ExecuteCommand);
        }

        private bool Can1ExecuteCommand(object obj)
        {
            return true;
        }

        private void Save1Product(object obj)
        {
            _selectedProduct = _productRepository.GetByUsername(_selectedProduct.Name);
            ListOfProducts.Add(_selectedProduct);
            uCStock.Close();

        }

        private void DeleteProductProduct(object obj)
        {
            ListOfProducts.Remove(SelectedProduct);
        }

        private bool CanExecuteEditProductCommand(object obj)
        {
            return true;
        }

        private bool CanExecuteProductCommand(object obj)
        {
            return true;
        }
        Views.DataEntry.ProductOrderDataEntry uCStock;
        private void AddProductOrder(object obj)
        {
            _selectedProduct = new ProductModel();
            uCStock = new Views.DataEntry.ProductOrderDataEntry();
            uCStock.DataContext = this;
            uCStock.ShowDialog();
        }


        public string SearchName
        {
            get => _searchName;
            set
            {
                _searchName = value;
                OnPropertyChanged(nameof(SearchName));
                FilterOrders();
            }
        }

        public ObservableCollection<OrderModel> ListOfOrders
        {
            get => _filteredOrders;
            set
            {
                _filteredOrders = value;
                OnPropertyChanged(nameof(ListOfOrders));
            }
        }
        public ObservableCollection<ProductModel> ListOfProducts
        {
            get => _listOfProducts;
            set
            {
                _listOfProducts = value;
                OnPropertyChanged(nameof(ListOfProducts));
            }
        }

        public OrderModel SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (value!= _selectedOrder)
                {
                        _selectedOrder = value;
                        if (_selectedOrder!= null)
                            _listOfProducts = new ObservableCollection<ProductModel>(_orderRepository.GetByID(_selectedOrder.IdOrder).IdProducts);
                        else
                            _listOfProducts = null;
                }
                
                OnPropertyChanged(nameof(ListOfProducts));
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }
        public ProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SaveCommand1 { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }

        Views.DataEntry.OrderDataEntry OrderDataEntry;
        private void FilterOrders()
        {
            if (string.IsNullOrEmpty(_searchName))
            {
                ListOfOrders = new ObservableCollection<OrderModel>(_orderRepository.GetByAll());
            }
            else
            {
                ListOfOrders = new ObservableCollection<OrderModel>(
                    _orderRepository.GetByAll().Where(p => p.IdOrder.ToString().Contains(_searchName.ToLower())));
            }
        }
        private void AddOrder(object obj)
        {
            ToSave = true;
            _selectedOrder = new OrderModel();
            ListOfProducts = new ObservableCollection<ProductModel>();
            OrderDataEntry = new OrderDataEntry();
            OrderDataEntry.DataContext = this;

            OrderDataEntry.ShowDialog();
        }

        private void EditProduct(object obj)
        {
            if (_selectedOrder != null)
            {
                ToSave = false;
                ListOfProducts = new ObservableCollection<ProductModel>(_selectedOrder.IdProducts);
                OrderDataEntry = new OrderDataEntry();
                OrderDataEntry.DataContext = this;

                OrderDataEntry.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a product!");
            }
        }

        private void DeleteProduct(object obj)
        {
            if (_selectedOrder != null && _selectedProduct == null)
            {
                _orderRepository.Delete(_selectedOrder);
                RefreshOrderList();
                ListOfProducts = new ObservableCollection<ProductModel>();
            }
            if (_selectedOrder != null && _selectedProduct != null)
            {
                _orderRepository.DeleteProduct(_selectedProduct,_selectedOrder);
                ListOfProducts = new ObservableCollection<ProductModel>(_orderRepository.GetByID(_selectedOrder.IdOrder).IdProducts);
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a product!");
            }
        }

        private void SaveProduct(object obj)
        {
            _selectedOrder.IdProducts = new List<ProductModel>(ListOfProducts);
            ClientRepository clientRepository = new ClientRepository();
            WorkerRepository workerRepository = new WorkerRepository();
            if (clientRepository.GetByID(_selectedOrder.IdClient) == null )
                System.Windows.MessageBox.Show("InValid idClient");
            else if (workerRepository.GetByID(_selectedOrder.IdWorker) == null)
            {
                System.Windows.MessageBox.Show("InValid idWorker");
            }
            else
            {
                if (ToSave)
                { 
                    _orderRepository.Add(_selectedOrder);
                }
                else
                {
                    _orderRepository.Update(_selectedOrder);
                }
                OrderDataEntry.Close();
            }
            ListOfOrders = new ObservableCollection<OrderModel>( _orderRepository.GetByAll());
            ListOfProducts = new ObservableCollection<ProductModel>();


        }
        private void RefreshOrderList()
        {
            var orders = _orderRepository.GetByAll();
            ListOfOrders = new ObservableCollection<OrderModel>(orders);
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