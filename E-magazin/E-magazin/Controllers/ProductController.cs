using E_magazin.Contracts;
using E_magazin.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_magazin.Controllers
{
    public class ProductController : BaseController
    {
        private IProductServices productServices;

        public ProductController(IProductServices _productServices)
        {
            productServices = _productServices;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Products(string productName, string category)
        {
            var model = productServices.Products().ToList();

            if (!string.IsNullOrEmpty(productName))
            {
                model = model.Where(x => x.Name.ToLower().Contains(productName.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                model = model.Where(x => x.CategoryName == category).ToList();
            }
            return View(model);
        }

        public IActionResult ProductDetails(int id)
        {
            var model = productServices.GetProductById(id);
            return View(model);
        }

        public IActionResult BuyProduct(int id)
        {
            return RedirectToAction("SellProduct","Product", new { id = id});
        }

        [HttpGet]
        public IActionResult SellProduct(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellProduct([FromForm] SoldProduct soldProduct, int id)
        {
            await productServices.SellProduct(soldProduct,id);
            return RedirectToAction("Checkout", "Product");
        }

    }
}
