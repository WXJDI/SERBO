using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface IAdminRepository
    {
        void Add(AdminModule user);
        void Update(AdminModule user);
        AdminModule GetByUsername(string username);
        AdminModule GetAdmin();
        void UpdateWithoutPasswordUsername(AdminModule admin);

        void UpdateOnlyPasswordUsername(AdminModule admin);

    }
}
