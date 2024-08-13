using Microsoft.AspNetCore.Mvc;
using GameScape.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;


namespace GameScape.Controllers
{

    [Authorize(Policy = "OnlyAdmin")]
    public class ProductController : Controller
    {    

        private readonly ILogger<ProductController> _logger;
        private readonly IWebHostEnvironment _env;
        private IProductRepository productRepository;


        




        public ProductController(ILogger<ProductController> logger, IWebHostEnvironment env, IProductRepository repo)
        {
            _logger = logger;
            _env = env;
            productRepository = repo;
        }

        
        [HttpPost]
        public IActionResult AddProduct(string ProductName, string gamefeatures, string trailerlink, string ProductType, int Quantity, List<IFormFile> files)
        {

            Product product = new Product();

            product.GameTitle = ProductName;
            product.ProductType = ProductType;
            product.Price = Convert.ToInt32(gamefeatures.Split(',')[0]);
            product.TrailerPath = trailerlink;
            product.Quantity = Quantity;

            int commaIndex = gamefeatures.IndexOf(',') + 1; 
            product.Features = gamefeatures.Substring(commaIndex); 

  
            string wwwrootPath = _env.WebRootPath;
            string FolderPath = Path.Combine(wwwrootPath, "ProductImages");

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            string screenshotspath = string.Empty;
            int i = 0;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(FolderPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    string imagepath = Path.Combine("~/ProductImages/", fileName);

                    //imagepath = filePath;



                    if (i == 0)
                    {
                        product.CoverPhotoPath = imagepath;

                    }
                    else if (i == 1)
                    {

                        product.ThumbnailPath = imagepath;

                    }
                    else
                    {
                        screenshotspath += imagepath;
                        if (i != files.Count)
                            screenshotspath += ",";
                    }

                    i++;


                }
            }

            product.ScreenshotPath = screenshotspath; 


            productRepository.Add(product);


            //return RedirectToAction("ViewProducts", "Product");
            return Json(new { success = true, message = "Product added successfully" });
        }



        [AllowAnonymous]
        public ViewResult ViewProducts()
        {
            //List<Product> products =(List < Product >) productsRepository.GetAll();
            return View(productRepository.GetAll());
        }





        public IActionResult Remove(int ID)
        {
            productRepository.Delete(ID);
            return RedirectToAction("ViewProducts");
        }









        // FIX THIS 


        [HttpPost]
        public IActionResult Edit(int id,string ProductName, int Price , int Quantity, string ProductType,string CoverPhotoPath,string ScreenshotPath, string Features, string ThumbnailPath, string TrailerPath)
        {

            // we need id as well
            Product product = new Product();
            product.Id = id;
          
            product.GameTitle = ProductName;
            product.Price = Price;
            product.Quantity = Quantity;
            product.ProductType = ProductType;


            product.Features = Features;
            product.CoverPhotoPath = CoverPhotoPath;
            product.ScreenshotPath = ScreenshotPath;
            product.ThumbnailPath = ThumbnailPath;
            product.TrailerPath = TrailerPath;

            productRepository.Update(product);



            return RedirectToAction("ViewProducts", "Product");
        }



        public ViewResult Edit(int id)
        {
            Product p = productRepository.Edit(id);
            return View("Edit", p);
        }



        public ViewResult GetEditPageById(int id)
        {
            Product p = productRepository.Edit(id);
            return View("Edit", p);
        }











        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
    }
}


















