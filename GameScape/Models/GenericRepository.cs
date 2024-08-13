using Dapper;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Data.SqlClient;
namespace GameScape.Models;




public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly string _connectionString;

    public GenericRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Add(TEntity entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var tableName = typeof(TEntity).Name;
            var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

            var columnNames = string.Join(",", properties.Select(p => p.Name));
            var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

            var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames});";

           
            connection.Execute(query, entity);

        }
    }

    public TEntity GetById(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var tableName = typeof(TEntity).Name;
            var primaryKey = "Id";

            var query = $"SELECT * FROM {tableName} WHERE {primaryKey} = @Id;";


            return connection.Query<TEntity>(query, new { Id = id }).FirstOrDefault();
        }
    }

    public IEnumerable<TEntity> GetAll()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var tableName = typeof(TEntity).Name;

            var query = $"SELECT * FROM {tableName};";
            return connection.Query<TEntity>(query).ToList();

        }
    }

    public void Update(TEntity entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var tableName = typeof(TEntity).Name;
            var primaryKey = "Id";

            var properties = typeof(TEntity).GetProperties().Where(p => p.Name != primaryKey);

            var setClause = string.Join(",", properties.Select(p => $"{p.Name} = @{p.Name}"));
            var query = $"UPDATE {tableName} SET {setClause} WHERE {primaryKey} = @{primaryKey};";

            connection.Execute(query, entity);

        }
    }

    public void Delete(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var tableName = typeof(TEntity).Name;
            var primaryKey = "Id";

            var query = $"DELETE FROM {tableName} WHERE {primaryKey} = @Id;";

            connection.Execute(query, new { Id = id });
        }
    }

    List<Product> IGenericRepository<TEntity>.get(string v)
    {
        throw new NotImplementedException();
    }
}
