using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using System.Windows.Controls;

namespace WpfApp1.Repositories
{
    public class OrderRepository : RepositoryBase , IOrderRepository
    {
        public void Add(OrderModel order)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select MAX(IDCOMMAND) from [COMMAND]";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order.IdOrder = int.Parse(reader[3].ToString()) + 1;
                    }
                }
            }
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                for (int i = 0; i < order.IdProducts.Count(); i++)
                {
                    command.CommandText = @"
                    INSERT INTO [COMMAND]
                    (IDPRODUCT,IDCLIENT, IDWORKER, IDCOMMAND, DATECOMMAND,TOTALPRICE) 
                    VALUES 
                    (@IdProduct,@IdClient, @IdWorker, @IdCommand, @DateCommand,TotalPrice);";
                    command.Parameters.Add("@IdProduct", SqlDbType.NVarChar).Value = order.IdProducts[i];
                    command.Parameters.Add("@IdClient", SqlDbType.NVarChar).Value = order.IdClient;
                    command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = order.IdWorker;
                    command.Parameters.Add("@IdCommand", SqlDbType.NVarChar).Value = order.IdOrder;
                    command.Parameters.Add("@DateCommand", SqlDbType.NVarChar).Value = order.DateOrder;
                    command.Parameters.Add("@TotalPrice", SqlDbType.NVarChar).Value = order.TotalPrice;

                    command.ExecuteNonQuery();
                }
                
            }
        }
        public void Update(OrderModel order)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                for (int i = 0;i<order.IdProducts.Count();i++)
                {
                    command.CommandText = @"
                    UPDATE [COMMAND]
                    SET IDPRODUCT = @IdProduct ,
                        IDCLIENT = @IdClient, 
                        IDWORKER = @IdWorker,
                        DATECOMMAND = @DateCommand,
                        TOTALPRICE = @TotalPrice ,
                    WHERE IDCOMMAND = @IdCommand";

                    command.Parameters.Add("@IdProduct", SqlDbType.NVarChar).Value = order.IdProducts[i];
                    command.Parameters.Add("@IdClient", SqlDbType.NVarChar).Value = order.IdClient;
                    command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = order.IdWorker;
                    command.Parameters.Add("@IdCommand", SqlDbType.NVarChar).Value = order.IdOrder;
                    command.Parameters.Add("@DateCommand", SqlDbType.NVarChar).Value = order.DateOrder;
                    command.Parameters.Add("@TotalPrice", SqlDbType.NVarChar).Value = order.TotalPrice;

                    command.ExecuteNonQuery();
                }
                
            }
        }
        public void Delete(OrderModel order)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                for (int i = 0; i < order.IdProducts.Count(); i++)
                {
                    command.CommandText = "DELETE FROM [COMMAND] WHERE IDCOMMAND = @IdCommand";
                    command.Parameters.Add("@IdCommand", SqlDbType.NVarChar).Value = order.IdOrder;           
                    command.ExecuteNonQuery();
                }
                connection.Open();
                command.Connection = connection;
                
            }
        }
        public OrderModel GetByID(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [COMMAND] WHERE IdCommand = @id";
                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        OrderModel order = new OrderModel()
                        {
                            IdOrder = int.Parse(reader[3].ToString()),
                            IdClient = int.Parse(reader[1].ToString()),
                            IdWorker = int.Parse(reader[2].ToString()),
                            TotalPrice = float.Parse(reader[5].ToString()),
                        };
                        order.IdProducts.Add(int.Parse(reader[0].ToString()));

                        while (reader.Read())
                        {
                            order.IdProducts.Add(int.Parse(reader[0].ToString()));
                        }
                        return order;
                    }
                    
                }
            }
            return null;
        }
        public List<OrderModel> GetByAll()
        {
            List<OrderModel> Wm = new List<OrderModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT DISTINCT IDCOMMAND FROM [COMMAND]";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrderModel order = new OrderModel();
                        order = GetByID(int.Parse(reader[0].ToString()));
                        Wm.Add(order);
                    }

                }
            }
            return Wm;
        }
        public List<ProductModel> GetProducts(OrderModel order)
        {
            return null;
        }
    }
}
