using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel user)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validAdmin,validWorker;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from [ADMIN] where Username=@username and Password=@password ";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validAdmin = command.ExecuteScalar() == null ? false : true;
            }
            using (var connection = GetConnection())
            using  (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from [WORKER] where Username=@username and Password=@password ";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validWorker = command.ExecuteScalar() == null ? false : true;
            }
            return (validAdmin || validWorker);
        }

        public void Delete(UserModel user)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [WORKER] where UserName=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            IdUser = reader[0].ToString(),
                            Username = reader[7].ToString(),
                            Password = string.Empty,
                            Name = reader[2].ToString(),
                            LastName = reader[1].ToString(),
                            Email = reader[4].ToString(),
                            NumTel = reader[5].ToString(),
                            Cin = reader[3].ToString(),
                            IsAdmin = false,
                            IsWorker = true
                        };
                    }
                }
                command.CommandText = "select *from [ADMIN] where UserName=@username";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            IdUser = reader[0].ToString(),
                            Username = reader[7].ToString(),
                            Password = string.Empty,
                            Name = reader[2].ToString(),
                            LastName = reader[1].ToString(),
                            Email = reader[4].ToString(),
                            NumTel = reader[5].ToString(),
                            Cin = reader[3].ToString(),
                            IsAdmin = false,
                            IsWorker = true
                        };
                    }
                }
            }
            return user;
        }
        public string GetUserRole(string username)
        {
            bool validAdmin ;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from [ADMIN] where Username=@username ";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                validAdmin = command.ExecuteScalar() == null ? false : true;
            }
            if (validAdmin)
                return "Admin";

            return "Worker";
        }

        public void Update(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
