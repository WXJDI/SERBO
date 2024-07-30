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
    public class OrderRepository : RepositoryBase ,     IOrderRepository
    {
        public void Add(OrderModel order)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Get the max IDCARD and calculate the new IdOrder
                using (var command = new SqlCommand("SELECT MAX(IDCOMMAND) FROM [COMMAND]", connection))
                {
                    var result = command.ExecuteScalar();
                    order.IdOrder = (result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 1;
                }

                // Insert into COMMAND table
                using (var command = new SqlCommand(@"
            INSERT INTO [COMMAND] (IDCOMMAND, CLI_IDUSER, IDUSER, TOTALPRICE) 
            VALUES (@IdCommand, @IdClient, @IdWorker, @TotalPrice);", connection))
                {
                    command.Parameters.Add("@IdClient", SqlDbType.NVarChar).Value = order.IdClient;
                    command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = order.IdWorker;
                    command.Parameters.Add("@IdCommand", SqlDbType.Int).Value = order.IdOrder;
                    command.Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = order.TotalPrice;
                    command.ExecuteNonQuery();
                }

                // Insert into PRODUCTCOMMAND table
                foreach (var product in order.IdProducts)
                {
                    using (var command = new SqlCommand(@"
                INSERT INTO [PRODUCTCOMMAND] (IDPRODUCT, IDCOMMAND, QUANTITY) 
                VALUES (@IdProduct, @IdCommand, @Quantity);", connection))
                    {
                        command.Parameters.Add("@IdProduct", SqlDbType.Int).Value = product.Id;
                        command.Parameters.Add("@IdCommand", SqlDbType.Int).Value = order.IdOrder;
                        command.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantite;
                        command.ExecuteNonQuery();
                    }
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
                    command.CommandText = @"
                    UPDATE [COMMAND]
                    SET CLI_IDUSER = @IdClient, 
                        IDUSER = @IdWorker,
                        TOTALPRICE = @TotalPrice 
                    WHERE IDCOMMAND = @IdCommand;";
                    command.Parameters.Add("@IdClient", SqlDbType.NVarChar).Value = order.IdClient;
                    command.Parameters.Add("@IdWorker", SqlDbType.NVarChar).Value = order.IdWorker;
                    command.Parameters.Add("@IdCommand", SqlDbType.NVarChar).Value = order.IdOrder;
                    command.Parameters.Add("@TotalPrice", SqlDbType.NVarChar).Value = order.TotalPrice;

                    command.ExecuteNonQuery();
                
            }
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                for (int i = 0; i < order.IdProducts.Count(); i++)
                {
                    command.CommandText = @"
                    UPDATE [PRODUCTCOMMAND]
                    SET Quantity = @quantite
                    WHERE IDPRODUCT = @idproduct
                    AND IDCOMMAND = @idCommand";

                    command.Parameters.Add("@idproduct", SqlDbType.NVarChar).Value = order.IdProducts[i].Id;
                    command.Parameters.Add("@quantite", SqlDbType.NVarChar).Value = order.IdProducts[i].Quantite;
                    command.Parameters.Add("@IdCommand", SqlDbType.NVarChar).Value = order.IdOrder;

                    command.ExecuteNonQuery();
                }

            }
        }
        public void Delete(OrderModel order)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Delete from PRODUCTCOMMAND table
                foreach (var product in order.IdProducts)
                {
                    using (var command = new SqlCommand("DELETE FROM [PRODUCTCOMMAND] WHERE IDCOMMAND = @IdCommand AND IDPRODUCT = @IdProduct", connection))
                    {
                        command.Parameters.Add("@IdCommand", SqlDbType.Int).Value = order.IdOrder;
                        command.Parameters.Add("@IdProduct", SqlDbType.Int).Value = product.Id;
                        command.ExecuteNonQuery();
                    }
                }

                // Delete from COMMAND table
                using (var command = new SqlCommand("DELETE FROM [COMMAND] WHERE IDCOMMAND = @IdCommand", connection))
                {
                    command.Parameters.Add("@IdCommand", SqlDbType.Int).Value = order.IdOrder;
                    command.ExecuteNonQuery();
                }
            }
        }

        public OrderModel GetByID(int id)
        {
            OrderModel order = null;
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
                        order = new OrderModel()
                        {
                            IdOrder = int.Parse(reader[2].ToString()),
                            IdClient = int.Parse(reader[0].ToString()),
                            IdWorker = int.Parse(reader[1].ToString()),
                            TotalPrice = float.Parse(reader[4].ToString())
                        };
                    }
                    
                }
            }
            ProductRepository _productRepository = new ProductRepository();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [PRODUCTCOMMAND] WHERE IdCommand = @id";
                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel product = new ProductModel()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Quantite = int.Parse(reader[2].ToString()),
                        };
                        product.Name = _productRepository.GetByID(product.Id).Name;
                        product.Price = _productRepository.GetByID(product.Id).Price;
                        order.IdProducts.Add(product);
                    }

                }
            }
            return order;
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
        public void DeleteProduct(ProductModel product, OrderModel order)
        {
            
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [PRODUCTCOMMAND] WHERE IDPRODUCT = @IdProduct AND IDCOMMAND = @IdCommand ";
                command.Parameters.Add("@IdProduct", SqlDbType.NVarChar).Value = product.Id;
                command.Parameters.Add("@IdCommand", SqlDbType.NVarChar).Value = order.IdOrder;
                command.ExecuteNonQuery();


            }
        }
        public List<OrderModel> GetByIdWorker(int id)
        {
            List<OrderModel> Wm = new List<OrderModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT DISTINCT IDCOMMAND FROM [COMMAND] WHERE IDUSER = @id";
                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
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
    }
}
