using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Controls;

namespace WpfApp1.Repositories
{
    public class WorkerRepository : RepositoryBase , IWorkerRepository
    {
        public void Add(WorkerModel worker)
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
                        worker.IdWorker = int.Parse(reader[0].ToString())+1;
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
                    (@IdWorker,@Name, @LastName, @Cin, @Email, @NumTel);
                    SET IDENTITY_INSERT [USER] ON;";
                command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = worker.IdWorker;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = worker.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = worker.LastName;
                command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = worker.Cin;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = worker.Email;
                command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = worker.NumTel;

                command.ExecuteNonQuery();
            }
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = @"
                    INSERT INTO [WORKER] 
                    (IDUSER,PRENOM, NOM, CIN, ADRESSMAIL, NUMTEL, Password, Username) 
                    VALUES 
                    (@IdWorker,@Name, @LastName, @Cin, @Email, @NumTel, @Password, @Username)";
                    command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = worker.IdWorker;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = worker.Name;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = worker.LastName;
                    command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = worker.Cin;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = worker.Email;
                    command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = worker.NumTel;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = worker.Password;
                    command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = worker.Username;

                    command.ExecuteNonQuery();
                }
            
        }
        public void Update(WorkerModel worker)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    UPDATE [WORKER]
                    SET PRENOM = @Name,
                        NOM = @LastName,
                        CIN = @Cin,
                        ADRESSMAIL = @Email,
                        NUMTEL = @NumTel,
                        Password = @Password,
                        Username = @Username
                    WHERE IDUSER = @IdWorker";

                command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = worker.IdWorker;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = worker.Name;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = worker.LastName;
                command.Parameters.Add("@Cin", SqlDbType.NVarChar).Value = worker.Cin;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = worker.Email;
                command.Parameters.Add("@NumTel", SqlDbType.NVarChar).Value = worker.NumTel;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = worker.Password;
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = worker.Username;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(WorkerModel worker)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [WORKER] WHERE IDUSER = @IdWorker";
                command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = worker.IdWorker;
                command.ExecuteNonQuery();
            }
        }
        public List<WorkerModel> GetByAll()
        {
            List<WorkerModel> Wm = new List<WorkerModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [WORKER]";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WorkerModel worker = new WorkerModel()
                        {
                            IdWorker = int.Parse(reader[0].ToString()),
                            Username = reader[7].ToString(),
                            Password = string.Empty,
                            Name = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Email = reader[4].ToString(),
                            NumTel = reader[5].ToString(),
                            Cin = reader[3].ToString(),
                        };
                        Wm.Add(worker);
                    }
                }
            }
            return Wm;
        }
        public WorkerModel GetByID(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [WORKER] WHERE IdWorker = @id";
                command.Parameters.Add("@idWorker", SqlDbType.NVarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        WorkerModel worker = new WorkerModel()
                        {
                            IdWorker = int.Parse(reader[0].ToString()),
                            Username = reader[7].ToString(),
                            Password = string.Empty,
                            Name = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Email = reader[4].ToString(),
                            NumTel = reader[5].ToString(),
                            Cin = reader[3].ToString(),
                        };
                        return worker;
                    }
                }
            }
            return null;
        }
        public WorkerModel GetByUsername(string username)
        {
            WorkerModel worker = null;
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
                        worker = new WorkerModel()
                        {
                            IdWorker = int.Parse(reader[0].ToString()),
                            Username = reader[7].ToString(),
                            Password = string.Empty,
                            Name = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Email = reader[4].ToString(),
                            NumTel = reader[5].ToString(),
                            Cin = reader[3].ToString(),
                        };
                    }
                }
            }
            return worker;
        }
    }
}
