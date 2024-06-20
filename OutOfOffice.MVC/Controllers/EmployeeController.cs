using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.Services;
using OutOfOffice.MVC.Extensions;

namespace OutOfOffice.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var employeeDto = await _employeeService.GetCreateEmployeeDtoAsync();

            return View(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                var createEmployeeDtoAfterValidation = await _employeeService.GetCreateEmployeeDtoAfterValidationAsync(createEmployeeDto);

                return View(createEmployeeDtoAfterValidation);
            }

            await _employeeService.CreateEmployeeAsync(createEmployeeDto);

            this.SetNotification("success", "Employee added successfully");

            return RedirectToAction(nameof(Create));
        }
    }
}
