using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class OrderModel
    {
        public int IdOrder { get; set;}

        public int IdClient { get; set; }
        public List<ProductModel> IdProducts { get; set; }
        public int IdWorker { get; set; }

        public DateTime DateOrder { get; set; }

        public int NomberProduct { get; set; }

        public float TotalPrice { get; set; }

        public OrderModel()
        {
            IdProducts = new List<ProductModel>();

            
        }
        public void CalculeTotalePrice()
        {
            float totalprice = 0;
            for (int i=0;i<IdProducts.Count;i++)
            {
                totalprice += IdProducts[i].Price * IdProducts[i].Quantite;
            }
            TotalPrice = totalprice;
        }

    }
}
