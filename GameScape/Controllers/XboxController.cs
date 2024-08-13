using GameScape.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameScape.Controllers
{
    public class XboxController : Controller
    {
        private IProductRepository productsRepository;

        public XboxController(IProductRepository prodRepo) { 
        
            productsRepository = prodRepo;
        }


        public IActionResult Index()
        {


            List<Product> products = productsRepository.get("Xbox");
            return View(products);
        }


        public IActionResult ShowSpecificProduct(int id)
        {
            Product product = productsRepository.Edit(id);
            return View("SpecificXbox", product);

        }



    }
}
