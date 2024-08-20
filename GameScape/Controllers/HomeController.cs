using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GameScape.Models;
using System.Reflection.Metadata.Ecma335;

namespace web_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductRepository productsRepository;


        public HomeController(ILogger<HomeController> logger, IProductRepository repo)
        {
            _logger = logger;
            productsRepository = repo;

        }

        public IActionResult Index()
        {
            return View();
        }





        public ViewResult ViewFavourites()
        {
            var cartItems = HttpContext.Session.Get<List<CartItems>>("FavProducts") ?? new List<CartItems>();
            return View(cartItems);
        }



        //[HttpPost]
        //public IActionResult Search(string query)
        //{

        //    List<Product> products = productsRepository.GetSearchResults(query);

        //    List<CartItems> cartItems = new List<CartItems>();
        //    foreach (Product p in products)
        //    {
        //        cartItems.Add(new CartItems { Id = p.Id, Name = p.GameTitle, Price = p.Price, Image = p.CoverPhotoPath, Category = p.ProductType, Quantity = p.Quantity });
        //    }

        //    return View(cartItems);
        //}


        [HttpGet]
        public IActionResult Search(string query)
        {
            List<Product> products = productsRepository.GetSearchResults(query);



            var searchResults = products.Select(p => new
            {
                Id = p.Id,
                Name = p.GameTitle,
                p.Price,
                Image = p.CoverPhotoPath,
                Category = p.ProductType,
                p.Quantity
            }).ToList();

            return Json(searchResults);
        }





        [HttpPost]
        public IActionResult AddToFavs(CartItems c)
        {

            var cartItems = HttpContext.Session.Get<List<CartItems>>("FavProducts") ?? new List<CartItems>();

            var item = cartItems.SingleOrDefault(x => x.Id == c.Id);

            if(item == null)
            {
                cartItems.Add(c);
                HttpContext.Session.Set("FavProducts", cartItems);
            }
            



            // Redirect based on category
            return c.Category switch
            {
                "Game" => RedirectToAction("ShowSpecificProduct", "Game", new { ID = c.Id }),
                "Console" => RedirectToAction("ShowSpecificProduct", "Console", new { ID = c.Id }),
                "Xbox" => RedirectToAction("ShowSpecificProduct", "Xbox", new { ID = c.Id }),
                "AllGame" => RedirectToAction("Index", "Game"),
                "AllConsole" => RedirectToAction("Index", "Console")
            };
        }




        public IActionResult NewDeals()
        {
            ViewBag.Special = "New Deals";
            return View("SpecialPage");
        }


        public IActionResult TimeLimited()
        {
            ViewBag.Special = "Time Limited";
            return View("SpecialPage");

        }


        public IActionResult Misc()
        {
            ViewBag.Special = "Miscellaneous";
            return View("SpecialPage");

        }








        public IActionResult AllGames()
        {
            return View();
        }


        public IActionResult AllConsoles()
        {
            return View();
        }

        public IActionResult GamePage()
        {
            return View();
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


        public IActionResult ConsolePage()
        {
            return View();
        }



        public IActionResult Xbox()
        {
            List<Product> products = productsRepository.get("Xbox");
            return View(products);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
