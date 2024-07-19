using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel user);
        void Update(UserModel user);
        void Delete(UserModel user);

        string GetUserRole(string username);
        UserModel GetByID(int id);
        UserModel GetByUsername(string username);
        List<UserModel> GetByAll();
    }
}
