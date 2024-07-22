using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Repositories
{
    public class CategoryRepository : RepositoryBase,ICategoryRepository
    {
        public void Add(CategoryModel category)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    INSERT INTO [CATEGORY] 
                    (IDCATEGORY,NAME, NOMBERPRODUCT,DESCRIPTION) 
                    VALUES 
                    (@IdCategory,@Name, @NomberProduct, @Description)";
                command.Parameters.Add("@IdCategory", SqlDbType.NVarChar).Value = category.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;
                command.Parameters.Add("@NomberProduct", SqlDbType.NVarChar).Value = category.NomberProduct;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = category.Description;
                

                command.ExecuteNonQuery();
            }
        }
        public void Update(CategoryModel category)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    UPDATE [CATEGORY]
                    SET NAME = @Name,
                        NOMBERPRODUCT = @NomberProduct,
                        DESCRIPTION = @Description,
                    WHERE IDCATEGORY = @IdCategory";

                command.Parameters.Add("@IdCategory", SqlDbType.NVarChar).Value = category.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;
                command.Parameters.Add("@NomberProduct", SqlDbType.NVarChar).Value = category.NomberProduct;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = category.Description;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(CategoryModel category)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [CATEGORY] WHERE IDCATEGORY = @IdCategory";
                command.Parameters.Add("@IdCategory", SqlDbType.NVarChar).Value = category.Id;
                command.ExecuteNonQuery();
            }
        }
        public CategoryModel GetByID(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT FROM [CATEGORY] WHERE IDCATEGORY = @IdCategory";
                command.Parameters.Add("@IdCategory", SqlDbType.NVarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        CategoryModel category = new CategoryModel()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            NomberProduct = int.Parse(reader[2].ToString()),
                            Description = reader[3].ToString(),
                        };
                        return category;
                    }
                }
            }
            return null;
        }
        public List<ProductModel> GetByAll(string name)
        {
            List<ProductModel> Wm = new List<ProductModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select PRODUCT(*) from [PRODUCT] p , [CATEGORY]  c, [ASSOCIATION_2] a where p.IdProduct = a.IdProduct and c.IdCategory = a.IdCategory and c.Name = @Name  ";
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel prod = new ProductModel()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            Price = float.Parse(reader[2].ToString()),
                            Quantite = int.Parse(reader[3].ToString()),
                        };
                        Wm.Add(prod);
                    }
                }
            }
            return Wm;
        }
    }
}
