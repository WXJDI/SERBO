using System;
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
                        _listOfProducts = new ObservableCollection<ProductModel>();
                        for (int i=0;i<SelectedOrder.IdProducts.Count;i++)
                        {
                            ProductModel productModel = new ProductModel();

                            productModel = _productRepository.GetByID(SelectedOrder.IdProducts[i]);
                            if (productModel != null)
                            {
                                _listOfProducts.Add(productModel);
                            }
                            
                        }
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

        Views.DataEntry.OrderDataEntry OrderDataEntry;
        private void FilterProducts()
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
        private void AddProduct(object obj)
        {
            ToSave = true;
            _selectedOrder = new OrderModel();

            OrderDataEntry = new OrderDataEntry();
            OrderDataEntry.DataContext = this;

            OrderDataEntry.ShowDialog();
        }

        private void EditProduct(object obj)
        {
            if (_selectedOrder != null)
            {
                ToSave = false;

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
            if (_selectedOrder != null)
            {
                _orderRepository.Delete(_selectedOrder);
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
                _orderRepository.Add(_selectedOrder);
            }
            else
            {
                _orderRepository.Update(_selectedOrder);
            }

            OrderDataEntry.Close();

            RefreshProductList();
        }

        private void RefreshProductList()
        {
            var clients = _orderRepository.GetByAll();
            ListOfOrders = new ObservableCollection<OrderModel>(clients);
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