using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OutOfOffice.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
