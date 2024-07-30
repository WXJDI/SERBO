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
    public class ProductRepository : RepositoryBase, IProductModel
    {
        public void Add(ProductModel prod)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select MAX(IDPRODUCT) from [PRODUCT]";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            prod.Id = int.Parse(reader[0].ToString()) + 1;
                        }
                        else
                        {
                            prod.Id = 1; // Si la table est vide, commencez par 1
                        }

                    }
                }
            }

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SET IDENTITY_INSERT [PRODUCT] ON;
                    INSERT INTO [PRODUCT] 
                    (IDPRODUCT,NAME, PRICE, QUANTITE) 
                    VALUES 
                    (@IdProduct,@Name, @PRICE, @QUANTITE)";
                command.Parameters.Add("@IdProduct", SqlDbType.NVarChar).Value = prod.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = prod.Name;
                command.Parameters.Add("@PRICE", SqlDbType.NVarChar).Value = prod.Price;
                command.Parameters.Add("@QUANTITE", SqlDbType.NVarChar).Value = prod.Quantite;
                command.ExecuteNonQuery();
            }

        }
        public void Update(ProductModel prod)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    UPDATE [PRODUCT]
                    SET NAME = @Name,
                        PRICE = @PRICE,
                        QUANTITE = @QUANTITE
                    WHERE IDPRODUCT = @IdProduct";

                command.Parameters.Add("@IdProduct", SqlDbType.NVarChar).Value = prod.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = prod.Name;
                command.Parameters.Add("@PRICE", SqlDbType.NVarChar).Value = prod.Price;
                command.Parameters.Add("@QUANTITE", SqlDbType.NVarChar).Value = prod.Quantite;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(ProductModel prod)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [PRODUCT] WHERE IDPRODUCT = @IdProduct";
                command.Parameters.Add("@IdProduct", SqlDbType.NVarChar).Value = prod.Id;
                command.ExecuteNonQuery();
            }
        }
        public List<ProductModel> GetByAll()
        {
            List<ProductModel> Wm = new List<ProductModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [PRODUCT]";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel worker = new ProductModel()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            Price = float.Parse( reader[2].ToString()),
                            Quantite = int.Parse(reader[3].ToString()),
                        };
                        Wm.Add(worker);
                    }
                }
            }
            return Wm;
        }
        public ProductModel GetByID(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [PRODUCT] WHERE IdProduct = @id";
                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ProductModel worker = new ProductModel()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            Price = float.Parse(reader[2].ToString()),
                            Quantite = int.Parse(reader[3].ToString()),
                        };
                        return worker;
                    }
                }
            }
            return null;
        }
        public ProductModel GetByUsername(string name)
        {
            ProductModel worker = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [PRODUCT] where Name=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = name;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        worker = new ProductModel()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            Price = float.Parse(reader[2].ToString()),
                            Quantite = int.Parse(reader[3].ToString()),
                        };
                    }
                }
            }
            return worker;
        }
        public bool HasOrders(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select IDCOMMAND from [PRODUCTCOMMAND] where idproduct = @id ";
                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }
    }
}
