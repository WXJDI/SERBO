using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface ICategoryRepository
    {
        void Add(CategoryModel category);
        void Update(CategoryModel category);
        void Delete(CategoryModel category);
        CategoryModel GetByID(int id);
        List<ProductModel> GetByAll(string name);
    }
}
