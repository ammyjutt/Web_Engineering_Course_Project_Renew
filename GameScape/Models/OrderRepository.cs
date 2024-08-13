using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using GameScape.Models;

namespace GameScape.Models
{

    // TODO: 
    public class OrderRepository :  IOrderRepository
    {

        //static string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"MyGameScape\";Integrated Security=True;";
        private IGenericRepository<Order> concreteGenRepo;

        
        public OrderRepository(IGenericRepository<Order> IGenRepo)
        {
            concreteGenRepo = IGenRepo;
        }

        public void Add(Order o)
        {
            throw new NotImplementedException();

        }

        public void AddCheckoutDetails(Order o)
        {
            concreteGenRepo.Add(o);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("In Order Repo / Delete");
        }

        

        public IEnumerable<Order> GetAll()
        {

            return concreteGenRepo.GetAll();

        }

        public Order GetById(int id)
        {
            throw new NotImplementedException("In Order Repo / Get By Id");

            //return concreteGenRepo.GetById(id);
        }

        public void Update(Order newOrder)
        {
            throw new NotImplementedException("In Order Repo / Update");

            //concreteGenRepo.Update(newOrder);
        }


        List<Product> IGenericRepository<Order>.get(string v)
        {
            throw new NotImplementedException();
        }

        
    }
}
