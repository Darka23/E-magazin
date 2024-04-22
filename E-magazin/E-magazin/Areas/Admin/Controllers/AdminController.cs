using E_magazin.Contracts;
using E_magazin.Data.Models;
using E_magazin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_magazin.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private IProductServices productServices;
        private ICategoryServices categoryServices;


        public AdminController(RoleManager<IdentityRole> _roleManager,
            UserManager<IdentityUser> _userManager,
            IProductServices _productServices,
            ICategoryServices _categoryServices)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            productServices = _productServices;
            categoryServices = _categoryServices;
        }
        public IActionResult AdminPanel()
        {
            var cashDesk = productServices.GetCashDesk();
            return View(cashDesk);
        }


        //  ROLES  //
        public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Administrator"
            });

            return Ok();
        }

        public async Task<IActionResult> AddUserToRole()
        {
            var user = userManager.Users.Where(x => x.UserName == "damyan").First();
            await userManager.AddToRoleAsync(user, "Administrator");
            return Ok();
        }

        //  ROLES  //

        //  PRODUCT  //

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewData["CategoryId"] = new SelectList(productServices.GetCategories(), "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(productServices.GetManufacturers(), "Id", "Name");
            ViewData["UnitOfMeasureId"] = new SelectList(productServices.GetUnitOfMeasures(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            await productServices.AddProduct(product);

            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var placeholder = await productServices.ProductForPlaceholder(id);
            ViewData["CategoryId"] = new SelectList(productServices.GetCategories(), "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(productServices.GetManufacturers(), "Id", "Name");
            ViewData["UnitOfMeasureId"] = new SelectList(productServices.GetUnitOfMeasures(), "Id", "Name");
            return View(placeholder);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct([FromForm] Product product)
        {
            if (await productServices.EditProduct(product))
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult DeleteProduct(int id)
        {
            productServices.DeleteProduct(id);

            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public IActionResult RestockProduct()
        {
            ViewData["ProductId"] = new SelectList(productServices.Products(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RestockProduct([FromForm] RestockViewModel restockProduct)
        {
            await productServices.RestockProduct(restockProduct);

            return RedirectToAction("AdminPanel", "Admin");
        }

        //  PRODUCT  //


        //  CATEGORY  //
        public IActionResult CategoryList()
        {
            var model = categoryServices.GetCategories();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] Category category)
        {
            await categoryServices.AddCategory(category);

            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory([FromForm] Category category)
        {
            if (await categoryServices.EditCategory(category))
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult DeleteCategory(int id)
        {
            if (categoryServices.DeleteCategory(id))
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

        }

        //  CATEGORY  //
    }
}
