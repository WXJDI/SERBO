using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface IClientRepository
    {
        void Add(ClientModel user);
        void Update(ClientModel user);
        void Delete(ClientModel user);
        ClientModel GetByID(int id);
        ClientModel GetByUsername(string username);
        List<ClientModel> GetByAll();
    }
}
