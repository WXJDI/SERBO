using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Repositories;
using WpfApp1.Views.DataEntry;

namespace WpfApp1.ViewModels.UCBusiness
{
    public class UCOrdersByWorkerBusiness : INotifyPropertyChanged
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
        private WorkerModel _currentWorker;


        public UCOrdersByWorkerBusiness()
        {
            _productRepository = new ProductRepository();
            _currentWorker = new WorkerModel();
            WorkerRepository workerRepository = new WorkerRepository();
            _currentWorker = workerRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            _orderRepository = new OrderRepository();
            _listOfOrders = new ObservableCollection<OrderModel>(_orderRepository.GetByIdWorker(_currentWorker.IdWorker));
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
            ProductModel prod = new ProductModel();
            prod = _productRepository.GetByUsername(_selectedProduct.Name);
            if (prod != null)
            {
                if (prod.Quantite >= _selectedProduct.Quantite)
                {
                    prod.Quantite = _selectedProduct.Quantite;
                    ListOfProducts.Add(prod);
                    _selectedProduct = null;
                    uCStock.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("The quantite offred not found at the stock !!");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("There is no product has that name !!");
            }

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
                if (value != _selectedOrder)
                {
                    _selectedOrder = value;
                    if (_selectedOrder != null)
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

        Views.DataEntry.OrderByWorkerDataEntry OrderByWorkerDataEntry;
        private void FilterOrders()
        {
            if (string.IsNullOrEmpty(_searchName))
            {
                ListOfOrders = new ObservableCollection<OrderModel>(_orderRepository.GetByIdWorker(_currentWorker.IdWorker));
            }
            else
            {
                ListOfOrders = new ObservableCollection<OrderModel>(
                    _orderRepository.GetByIdWorker(_currentWorker.IdWorker).Where(p => p.IdOrder.ToString().Contains(_searchName.ToLower())));
            }
        }
        private void AddOrder(object obj)
        {
            ToSave = true;
            _selectedOrder = new OrderModel();
            ListOfProducts = new ObservableCollection<ProductModel>();
            OrderByWorkerDataEntry = new OrderByWorkerDataEntry();
            OrderByWorkerDataEntry.DataContext = this;

            OrderByWorkerDataEntry.ShowDialog();
        }

        private void EditProduct(object obj)
        {
            if (_selectedOrder != null)
            {
                ToSave = false;
                ListOfProducts = new ObservableCollection<ProductModel>(_selectedOrder.IdProducts);
                for (int i = 0; i < ListOfProducts.Count(); i++)
                {
                    ProductModel product = new ProductModel(ListOfProducts[i]);

                    product.Quantite = _productRepository.GetByUsername(ListOfProducts[i].Name).Quantite + product.Quantite;

                    _productRepository.Update(product);

                }
                OrderByWorkerDataEntry = new OrderByWorkerDataEntry();
                OrderByWorkerDataEntry.DataContext = this;

                OrderByWorkerDataEntry.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a product!");
            }
        }

        private void DeleteProduct(object obj)
        {
            if (_selectedProduct != null)
            {
                ProductModel product = new ProductModel(SelectedProduct);

                product.Quantite = _productRepository.GetByUsername(SelectedProduct.Name).Quantite + product.Quantite;

                _productRepository.Update(product);
                _orderRepository.DeleteProduct(SelectedProduct, _selectedOrder);
                ListOfProducts = new ObservableCollection<ProductModel>(_orderRepository.GetByID(SelectedOrder.IdOrder).IdProducts);
                if (ListOfProducts.Count == 0)
                {
                    _orderRepository.Delete(SelectedOrder);
                    ListOfOrders = new ObservableCollection<OrderModel>(_orderRepository.GetByAll());
                }
            }
            else if (_selectedOrder != null )
            {
                for (int i = 0; i < SelectedOrder.IdProducts.Count(); i++)
                {
                    ProductModel product = new ProductModel(SelectedOrder.IdProducts[i]);

                    product.Quantite = _productRepository.GetByUsername(SelectedOrder.IdProducts[i].Name).Quantite + product.Quantite;

                    _productRepository.Update(product);

                }
                _orderRepository.Delete(SelectedOrder);
                RefreshOrderList();
                ListOfProducts = new ObservableCollection<ProductModel>();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a product or an order !");
            }
        }

        private void SaveProduct(object obj)
        {
            _selectedOrder.IdProducts = new List<ProductModel>(ListOfProducts);
            _selectedOrder.CalculeTotalePrice();
            ClientRepository clientRepository = new ClientRepository();
            _selectedOrder.IdWorker = _currentWorker.IdWorker;
            if (clientRepository.GetByID(_selectedOrder.IdClient) == null)
                System.Windows.MessageBox.Show("InValid idClient");
            else
            {
                for (int i = 0; i < ListOfProducts.Count(); i++)
                {
                    ProductModel product = new ProductModel(ListOfProducts[i]);

                    product.Quantite = _productRepository.GetByUsername(ListOfProducts[i].Name).Quantite - product.Quantite;

                    _productRepository.Update(product);

                }
                if (ToSave)
                {
                    _orderRepository.Add(_selectedOrder);
                }
                else
                {
                    _orderRepository.Update(_selectedOrder);
                }
                OrderByWorkerDataEntry.Close();
            }
            ListOfOrders = new ObservableCollection<OrderModel>(_orderRepository.GetByIdWorker(_currentWorker.IdWorker));
            ListOfProducts = new ObservableCollection<ProductModel>();


        }
        private void RefreshOrderList()
        {
            var orders = _orderRepository.GetByIdWorker(_currentWorker.IdWorker);
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
