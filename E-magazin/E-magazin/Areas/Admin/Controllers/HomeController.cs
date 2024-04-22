using Microsoft.AspNetCore.Mvc;

namespace E_magazin.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
