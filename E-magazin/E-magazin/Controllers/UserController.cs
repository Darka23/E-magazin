using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_magazin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public UserController(RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _userManager)
        {
            roleManager = _roleManager;
            userManager= _userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

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
    }
}
