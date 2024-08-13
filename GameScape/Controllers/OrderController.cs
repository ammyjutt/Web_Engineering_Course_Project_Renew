using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GameScape.Models;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Http;
using GameScape.Models;


namespace web_project.Controllers
{
    public class OrderController : Controller
    {



        private IOrderRepository orderRepository;


        public OrderController(IOrderRepository repo)
        {
            orderRepository = repo;
        }





        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Checkout(String address, String PostalCode, String cardNo, String exp, String cvv)
        {

            Order orderDetails = new Order { orderId = 0, adress = address, postalCode = PostalCode, cardNumber = cardNo, expiration = exp, cvv = cvv };

            orderRepository.AddCheckoutDetails(orderDetails);

            return RedirectToAction("Index", "Home");

        }

        public IActionResult DisplayAll ()
        {

            List<Order> orders = new List<Order>();

            orders = (List<Order>) orderRepository.GetAll();

            return View(orders);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public ViewResult ViewCart()
        {
            var cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();
            return View(cartItems);
        }



        [HttpPost]
        public IActionResult AddToCart(CartItems c)
        {

            var cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();

            var item = cartItems.SingleOrDefault(x => x.Id == c.Id);
            if (item != null)
            {
                item.Quantity += c.Quantity;
            }
            else
            {
                cartItems.Add(c);
            }

            HttpContext.Session.Set("CartProducts", cartItems);

            // Update the cookie with the correct cart item count
            int currentCartItemCount = 0;
            if (HttpContext.Request.Cookies.TryGetValue("cartItemCount", out var cartItemCountStr))
            {
                currentCartItemCount = Convert.ToInt32(cartItemCountStr);
            }
            int newCartItemCount  = currentCartItemCount + c.Quantity;
            HttpContext.Response.Cookies.Append("cartItemCount", newCartItemCount.ToString());


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


        public IActionResult removefromCart(int ID)
        {

            var cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();
            var item = cartItems.SingleOrDefault(x => x.Id == ID);
            cartItems.Remove(item);
            if (cartItems.Count == 0)
            {
                HttpContext.Response.Cookies.Delete("cartItemCount");
            }
            else
            {
                int currentCartItemCount = 0;
                if (HttpContext.Request.Cookies.TryGetValue("cartItemCount", out var cartItemCountStr))
                {
                    currentCartItemCount = Convert.ToInt32(cartItemCountStr);
                }
                int newCartItemCount = currentCartItemCount - item.Quantity;
                HttpContext.Response.Cookies.Append("cartItemCount", newCartItemCount.ToString());
            }
            HttpContext.Session.Set("CartProducts", cartItems);

            return RedirectToAction("viewCart");
        }


        public IActionResult OrderPlaced()
        {
            return View();
        }



        public ViewResult ViewOrdersDetails()
        {

            CartItems c = new CartItems();
            c.Id = 2;
            c.Quantity = 29;
            c.Price = 234;
            c.Category = "Console";

            Order o = new Order();

            o.orderId = 1;
            o.cardNumber = "1234 5678 9012 3456";
            o.adress = "1234 5th Avenue";
            o.postalCode = "12345";

            o.cvv = "1342";
            o.expiration = "12/23";


            CartOrder co = new CartOrder(c ,o);


            return View(co);
        }
    }
}














