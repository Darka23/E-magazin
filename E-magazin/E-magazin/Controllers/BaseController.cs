using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_magazin.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
