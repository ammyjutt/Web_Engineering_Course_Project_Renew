//using AspNetCore;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;

namespace GameScape.Models
{
    public class ProductRepository : IProductRepository
    {

        static string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"MyGameScape\";Integrated Security=True;";
        IGenericRepository<Product> concreteGenRepo;

        public ProductRepository(IGenericRepository<Product> IGenRepo)
        {
            concreteGenRepo = IGenRepo;
        }

        public void Add(Product p)
        {
            concreteGenRepo.Add(p);
        }

        public void Delete(int id)
        {
            concreteGenRepo.Delete(id);
        }

        public List<Product> get(string category)
        {
            List<Product> products = new List<Product>();
            SqlConnection connection = new SqlConnection(connStr);
            connection.Open();
            string query = $"select * from Product where ProductType = @category";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("category", category);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(rd[0]);
                product.ThumbnailPath = Convert.ToString(rd[1]);
                product.GameTitle = Convert.ToString(rd[2]);
                product.CoverPhotoPath = Convert.ToString(rd[3]);
                product.Price = Convert.ToInt32(rd[4]);

                product.Features = Convert.ToString(rd[5]);
                product.ScreenshotPath = Convert.ToString(rd[6]);
                product.TrailerPath = Convert.ToString(rd[7]);
                product.ProductType = Convert.ToString(rd[8]);
                product.Quantity = 999;

                products.Add(product);
            }
            return products;
        }

        public void Update(Product product)
        {
            concreteGenRepo.Update(product);
        }

        public Product Edit(int ID)
        {
            SqlConnection connection = new SqlConnection(connStr);
            connection.Open();
            string query = $"select * from Product where id = @id"; // Use parameter

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", ID); // Add name as parameter

            SqlDataReader rd = cmd.ExecuteReader();

            Product product = null;
            if (rd.HasRows)
            {
                rd.Read(); // Read the first row if data exists
                product = new Product();
                product.Id = Convert.ToInt32(rd[0]);

                product.Id = Convert.ToInt32(rd[0]);
                product.ThumbnailPath = Convert.ToString(rd[1]);
                product.GameTitle = Convert.ToString(rd[2]);
                product.CoverPhotoPath = Convert.ToString(rd[3]);
                product.Price = Convert.ToInt32(rd[4]);

                product.Features = Convert.ToString(rd[5]);
                product.ScreenshotPath = Convert.ToString(rd[6]);
                product.TrailerPath = Convert.ToString(rd[7]);
                product.ProductType = Convert.ToString(rd[8]);
                product.Quantity = Convert.ToInt32(rd[9]);

            }

            rd.Close();
            connection.Close();
            return product;

        }

        public IEnumerable<Product> GetAll()
        {
            return concreteGenRepo.GetAll();
        }

        public List<Product> GetSearchResults(string searchQuery)
        {
            List<Product> ans = new List<Product>();
            var allProducts = GetAll();

            ans = allProducts.Where(p => p.GameTitle.ToLower().StartsWith(searchQuery.ToLower())).ToList();

            return ans;
        }

        public Product GetById(int id)
        {
            return concreteGenRepo.GetById(id);
        }
    }
}




