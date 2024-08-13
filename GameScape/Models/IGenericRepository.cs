
namespace GameScape.Models
{
    public interface IGenericRepository<TEntity>
    {

        void Add(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Delete(int id);
        List<Product> get(string v);
    }
}
