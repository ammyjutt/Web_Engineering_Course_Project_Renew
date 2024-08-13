using System.Collections.Generic;

namespace GameScape.Models
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> get(string category);
        Product Edit(int id);
        List<Product> GetSearchResults(string searchQuery);
    }
}
