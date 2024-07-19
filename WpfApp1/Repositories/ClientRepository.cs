using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Repositories
{
    public class ClientRepository : RepositoryBase, IClientRepository
    {
        public void Add(ClientModel client)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select COUNT(*) from [USER]";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client.IdClient = int.Parse(reader[0].ToString()) + 1;
                    }
                }
            }
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SET IDENTITY_INSERT [USER] ON;
                    INSERT INTO [USER]
                    (IDUSER,PRENOM, NOM, CIN, ADRESSMAIL, NUMTEL) 
                    VALUES 
                    (@IdClient,@Name, @LastName, @Cin, @Email, @NumTel);
                    SET IDENTITY_INSERT [USER] ON;";
                command.Parameters.Add("@IdClient", SqlDbType.NVarChar).Value = client.IdClient;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = client.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = client.LastName;
                command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = client.Cin;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = client.Email;
                command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = client.NumTel;

                command.ExecuteNonQuery();
            }
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    INSERT INTO [CLIENT] 
                    (IDUSER,PRENOM, NOM, CIN, ADRESSMAIL, NUMTEL) 
                    VALUES 
                    (@IdClient,@Name, @LastName, @Cin, @Email, @NumTel)";
                command.Parameters.Add("@IdClient", SqlDbType.NVarChar).Value = client.IdClient;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = client.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = client.LastName;
                command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = client.Cin;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = client.Email;
                command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = client.NumTel;
                command.ExecuteNonQuery();
            }

        }
        public void Update(ClientModel client)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    UPDATE [CLIENT]
                    SET PRENOM = @Name,
                        NOM = @LastName,
                        CIN = @Cin,
                        ADRESSMAIL = @Email,
                        NUMTEL = @NumTel
                    WHERE IDUSER = @IdClient";

                command.Parameters.Add("@IdClient", SqlDbType.NVarChar).Value = client.IdClient;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = client.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = client.LastName;
                command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = client.Cin;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = client.Email;
                command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = client.NumTel;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(ClientModel client)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [CLIENT] WHERE IDUSER = @IdClient";
                command.Parameters.Add("@IdClient", SqlDbType.NVarChar).Value = client.IdClient;
                command.ExecuteNonQuery();
            }
        }
        public List<ClientModel> GetByAll()
        {
            List<ClientModel> Wm = new List<ClientModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [CLIENT]";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClientModel client = new ClientModel()
                        {
                            IdClient = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Email = reader[4].ToString(),
                            NumTel = reader[5].ToString(),
                            Cin = reader[3].ToString(),
                        };
                        Wm.Add(client);
                    }
                }
            }
            return Wm;
        }
        public ClientModel GetByID(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [CLIENT] WHERE IdClient = @id";
                command.Parameters.Add("@IdClient", SqlDbType.NVarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ClientModel client = new ClientModel()
                        {
                            IdClient = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Email = reader[4].ToString(),
                            NumTel = reader[5].ToString(),
                            Cin = reader[3].ToString(),
                        };
                        return client;
                    }
                }
            }
            return null;
        }
        public ClientModel GetByUsername(string username)
        {
            ClientModel client = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [USER] where UserName=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new ClientModel()
                        {
                            IdClient = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Email = reader[4].ToString(),
                            NumTel = reader[5].ToString(),
                            Cin = reader[3].ToString(),
                        };
                    }
                }
            }
            return client;
        }
    }
}

