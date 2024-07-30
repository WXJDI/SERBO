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

    public class UCStockBusiness : INotifyPropertyChanged
    {
        public bool ToSave { get; set; }
        private string _searchName;
        private ObservableCollection<ProductModel> _listOfProducts;
        private ObservableCollection<ProductModel> _filteredProducts;
        private ProductRepository _productRepository;
        private ProductModel _selectedProduct;

        public UCStockBusiness()
        {
            _productRepository = new ProductRepository();
            _listOfProducts = new ObservableCollection<ProductModel>(_productRepository.GetByAll());
            _filteredProducts = new ObservableCollection<ProductModel>(_listOfProducts);
            AddCommand = new ViewModelCommand(AddProduct, CanExecuteCommand);
            EditCommand = new ViewModelCommand(EditProduct, CanExecuteCommand);
            DeleteCommand = new ViewModelCommand(DeleteProduct, CanExecuteCommand);
            SaveCommand1 = new ViewModelCommand(SaveProduct, CanExecuteCommand);
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

        public ObservableCollection<ProductModel> ListOfProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(ListOfProducts));
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
        public ICommand SaveCommand1 { get; set; }

        private void FilterProducts()
        {
            if (string.IsNullOrEmpty(_searchName))
            {
                ListOfProducts = new ObservableCollection<ProductModel>(_productRepository.GetByAll());
            }
            else
            {
                ListOfProducts = new ObservableCollection<ProductModel>(
                    _productRepository.GetByAll().Where(p => p.Name.ToLower().Contains(_searchName.ToLower())));
            }
        }
        Views.DataEntry.ProductDataEntry productDataEntry;
        private void AddProduct(object obj)
        {
            ToSave = true;
            _selectedProduct = new ProductModel();

            productDataEntry = new ProductDataEntry();
            productDataEntry.DataContext = this;

            productDataEntry.ShowDialog();
        }

        private void EditProduct(object obj)
        {
            if (_selectedProduct != null)
            {
                ToSave = false;

                productDataEntry = new ProductDataEntry();
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
            if (_selectedProduct != null)
            {
                if (!(_productRepository.HasOrders(_selectedProduct.Id)))
                {
                    _productRepository.Delete(_selectedProduct);
                    RefreshProductList();
                }
                else
                {
                    System.Windows.MessageBox.Show("PRODUCT can't be deleted !! Please Delete Orders Associated with this Product !!!");
                }
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
                _productRepository.Add(_selectedProduct);
            }
            else
            {
                _productRepository.Update(_selectedProduct);
            }
            productDataEntry.Close();

            RefreshProductList();
        }

        private void RefreshProductList()
        {
            var products = _productRepository.GetByAll();
            ListOfProducts = new ObservableCollection<ProductModel>(products);
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
