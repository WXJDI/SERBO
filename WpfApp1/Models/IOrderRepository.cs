using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface IOrderRepository
    {
        void Add(OrderModel order);
        void Update(OrderModel order);
        void Delete(OrderModel order);
        OrderModel GetByID(int id);
        List<OrderModel> GetByAll();
        void DeleteProduct(ProductModel product, OrderModel order);

    }
}
