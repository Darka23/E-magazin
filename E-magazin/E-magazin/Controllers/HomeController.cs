using E_magazin.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_magazin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryServices categoryServices;

        public HomeController(ILogger<HomeController> logger, ICategoryServices _categoryServices)
        {
            _logger = logger;
            categoryServices = _categoryServices;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

    }
}
