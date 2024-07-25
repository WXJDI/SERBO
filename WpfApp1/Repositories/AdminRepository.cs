using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security;

namespace WpfApp1.Repositories
{
    public class AdminRepository : RepositoryBase , IAdminRepository
    {
        public void Add(AdminModule admin)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SET IDENTITY_INSERT [USER] ON;
                    INSERT INTO [USER]
                    (IDUSER,Name, Lastname, CIN, ADRESSMAIL, NUMTEL) 
                    VALUES 
                    (@IdWorker,@Name, @LastName, @Cin, @Email, @NumTel);
                    SET IDENTITY_INSERT [USER] ON;";
                command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = admin.IdWorker;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = admin.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = admin.LastName;
                command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = admin.Cin;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = admin.Email;
                command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = admin.NumTel;

                command.ExecuteNonQuery();
            }
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    INSERT INTO [ADMIN] 
                    (IDUSER,Name, Lastname, CIN, ADRESSMAIL, NUMTEL, Password, Username) 
                    VALUES 
                    (@IdWorker,@Name, @LastName, @Cin, @Email, @NumTel, @Password, @Username)";
                command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = admin.IdWorker;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = admin.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = admin.LastName;
                command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = admin.Cin;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = admin.Email;
                command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = admin.NumTel;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = admin.Password;
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = admin.Username;

                command.ExecuteNonQuery();
            }
        }
        public void Update(AdminModule admin)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    UPDATE [ADMIN]
                    SET Name = @Name,
                        LastName = @LastName,
                        CIN = @Cin,
                        ADRESSMAIL = @Email,
                        NUMTEL = @NumTel,
                        Password = @Password,
                        Username = @Username
                    WHERE IDUSER = @IdWorker";

                command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = admin.IdWorker;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = admin.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = admin.LastName;
                command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = admin.Cin;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = admin.Email;
                command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = admin.NumTel;
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = admin.Username;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = admin.Password;


                command.ExecuteNonQuery();
            }
        }
        public void UpdateOnlyPasswordUsername(AdminModule admin)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    UPDATE [ADMIN]
                    SET Password = @Password,
                        Username = @Username
                    WHERE IDUSER = @IdWorker";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = admin.Username;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = admin.Password;


                command.ExecuteNonQuery();
            }
        }
        public void UpdateWithoutPasswordUsername(AdminModule admin)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    UPDATE [ADMIN]
                    SET Name = @Name,
                        LastName = @LastName,
                        CIN = @Cin,
                        ADRESSMAIL = @Email,
                        NUMTEL = @NumTel
                    WHERE IDUSER = @IdWorker";

                command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = admin.IdWorker;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = admin.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = admin.LastName;
                command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = admin.Cin;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = admin.Email;
                command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = admin.NumTel;


                command.ExecuteNonQuery();
            }
        }
        public AdminModule GetByUsername(string username)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [ADMIN] WHERE Username = @Username";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var admin = new AdminModule()
                        {
                            IdWorker = int.Parse(reader[0].ToString()),
                            Username = reader[7].ToString(),
                            Password = string.Empty,
                            Name = reader[2].ToString(),
                            LastName = reader[1].ToString(),
                            Email = reader[4].ToString(),
                            NumTel = reader[5].ToString(),
                            Cin = reader[3].ToString(),
                        };
                        return admin;
                    }
                }
            }
            return null;
        }
            public AdminModule GetAdmin()
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select *from [ADMIN] ";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var admin = new AdminModule()
                            {
                                IdWorker = int.Parse(reader[0].ToString()),
                                Username = reader[7].ToString(),
                                Password = string.Empty,
                                Name = reader[2].ToString(),
                                LastName = reader[1].ToString(),
                                Email = reader[4].ToString(),
                                NumTel = reader[5].ToString(),
                                Cin = reader[3].ToString(),
                            };
                            return admin;
                        }
                    }
                }
                return null;
            }
    }
}
