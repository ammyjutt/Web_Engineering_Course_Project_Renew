using GameScape.Models;
using Microsoft.AspNetCore.Mvc;


namespace GameScape.Controllers
{
    public class ConsoleController : Controller
    {

        private IProductRepository productsRepository;

        public ConsoleController(IProductRepository repo)
        {
            productsRepository = repo;
        }

        public IActionResult Index()
        {

            //ProductRepository productsRepository = new ProductRepository();
            List<Product> products = productsRepository.get("Console");
            return View(products);
        }


        public IActionResult ShowSpecificProduct(int id)
        {
            Product product = productsRepository.Edit(id);
            return View("SpecificConsole", product);

        }



    }
}
