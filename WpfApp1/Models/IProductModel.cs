using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface IProductModel
    {
        void Add(ProductModel prod);
        void Update(ProductModel prod);
        void Delete(ProductModel prod);
        List<ProductModel> GetByAll();
        ProductModel GetByID(int id);
        ProductModel GetByUsername(string name);
    }
}
