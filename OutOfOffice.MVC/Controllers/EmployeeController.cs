using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Application.Services;

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
            var employeeDto = await _employeeService.GetCreateEmployeeDto();

            return View(employeeDto);
        }
    }
}
