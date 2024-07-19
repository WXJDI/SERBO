using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface IWorkerRepository
    {
        void Add(WorkerModel user);
        void Update(WorkerModel user);
        void Delete(WorkerModel user);
        List<WorkerModel> GetByAll();
        WorkerModel GetByID(int id);
        WorkerModel GetByUsername(string username);
    }
}
