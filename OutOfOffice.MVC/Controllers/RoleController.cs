using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Application.Services;
using OutOfOffice.Domain.Models;
using OutOfOffice.MVC.Extensions;

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

        [HttpPost]
        public async Task<IActionResult> Index(UserRoleDto userRoleDto)
        {
            if (!ModelState.IsValid)
            {
                var userRoleDtoAfterValidation = await _userRoleService.GetViewDataAsync();
                userRoleDtoAfterValidation.SelectedUserId = userRoleDto.SelectedUserId;
                userRoleDtoAfterValidation.SelectedRoleId = userRoleDto.SelectedRoleId;

                return View(userRoleDtoAfterValidation);
            }

            await _userRoleService.UpdateUserRoleAsync(userRoleDto);

            this.SetNotification("success", "Role updated successfully");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleByUserId(string id)
        {
            var data = await _userRoleService.GetRoleByUserId(id);

            return Ok(data);
        }
    }
}
