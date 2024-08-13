using System.Collections.Generic;


namespace GameScape.Models
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        void Add(Order order);
        void AddCheckoutDetails(Order order);
        void Delete(int id);
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        void Update(Order order);
    }
}