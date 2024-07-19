using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class ClientModel
    {
        public int IdClient { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string NumTel { get; set; }

        public string Cin { get; set; }

        public DateTime Date { get; set; }
    }
}
