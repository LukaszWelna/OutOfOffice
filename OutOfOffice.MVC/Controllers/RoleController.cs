using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Application.Services;

namespace OutOfOffice.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        private readonly IUserRoleService _userRoleService;

        public RoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userRoleDtos = await _userRoleService.GetViewDataAsync();

            return View(userRoleDtos);
        }
    }
}
