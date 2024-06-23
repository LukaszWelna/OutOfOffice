using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.Services;
using OutOfOffice.MVC.Extensions;

namespace OutOfOffice.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUserRoleService _userRoleService;

        public EmployeeController(IEmployeeService employeeService, IUserRoleService userRoleService)
        {
            _employeeService = employeeService;
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]string searchPhrase, [FromQuery]string sortOrder)
        {
            ViewData["FullNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "fullNameDesc" : "";
            ViewData["EmailSortParam"] = sortOrder == "emailAsc" ? "emailDesc" : "emailAsc";
            ViewData["SubdivisionSortParam"] = sortOrder == "subdivisionAsc" ? "subdivisionDesc" : "subdivisionAsc";
            ViewData["PositionSortParam"] = sortOrder == "positionAsc" ? "positionDesc" : "positionAsc";
            ViewData["StatusSortParam"] = sortOrder == "statusActive" ? "statusInactive" : "statusActive";
            ViewData["PeoplePartnerSortParam"] = sortOrder == "peoplePartnerAsc" ? "peoplePartnerDesc" : "peoplePartnerAsc";
            ViewData["ProjectTypeSortParam"] = sortOrder == "projectTypeAsc" ? "projectTypeDesc" : "projectTypeAsc";
            ViewData["DaysOffSortParam"] = sortOrder == "daysOffAsc" ? "daysOffDesc" : "daysOffAsc";

            ViewData["CurrentSearchPhrase"] = searchPhrase;
            ViewData["CurrentSortOrder"] = sortOrder;

            var allEmployees = await _employeeService.GetAllEmployeesAsync(searchPhrase, sortOrder);

            return View(allEmployees);
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editEmployeeDto = await _employeeService.GetEditEmployeeDtoByIdAsync(id);

            return View(editEmployeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeDto editEmployeeDto)
        {
            if(!ModelState.IsValid)
            {
                var editEmployeeDtoAfterValidation = await _employeeService.GetEditEmployeeDtoAfterValidationAsync(editEmployeeDto);

                return View(editEmployeeDtoAfterValidation);
            }

            await _employeeService.EditEmployeeById(editEmployeeDto);

            this.SetNotification("success", "Employee edited successfully");

            return RedirectToAction("Index", "Employee");
        }
    }
}
