using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class WorkerModel
    {
        public int IdWorker { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string NumTel { get; set; }

        public string Cin { get; set; }

        public DateTime Date { get; set; }
    }
}
