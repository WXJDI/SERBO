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
        public List<int> IdProducts { get; set; }
        public int IdWorker { get; set; }

        public DateTime DateOrder { get; set; }

        public int NomberProduct { get; set; }

        public float TotalPrice { get; set; }
    }
}
