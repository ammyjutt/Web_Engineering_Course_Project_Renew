using GameScape.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameScape.Controllers
{
    public class GameController : Controller
    {



        private IProductRepository productsRepository;

        public GameController(IProductRepository repo)
        {
            productsRepository = repo;
        }


        public IActionResult Index()
        {

            var products = productsRepository.get("Game");
            return View(products);
        }


        public IActionResult ShowSpecificProduct(int id)
        {
            Product product = productsRepository.Edit(id);
            return View("SpecificGame", product);

        }




        public IActionResult ActionPage()
        {

            return View();
        }

        public IActionResult SportsPage()
        {
            return View();
        }

        public IActionResult RacingPage()
        {
            return View();
        }

        public IActionResult StrategyPage()
        {
            return View();
        }

        public IActionResult Multiplayerpage()
        {
            return View();
        }

        public IActionResult SurvivalPage()
        {
            return View();
        }

        public IActionResult PuzzlePage()
        {
            return View();
        }

        public IActionResult KidsPage()
        {
            return View();
        }







    }
}
