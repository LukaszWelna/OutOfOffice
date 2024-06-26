using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.LeaveRequest;
using OutOfOffice.Application.Services;
using OutOfOffice.MVC.Extensions;

namespace OutOfOffice.MVC.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string searchPhrase, [FromQuery] string sortOrder)
        {
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "IdDesc" : "";
            ViewData["EmployeeSortParam"] = sortOrder == "employeeAsc" ? "employeeDesc" : "employeeAsc";
            ViewData["AbsenceReasonSortParam"] = sortOrder == "absenceReasonAsc" ? "absenceReasonDesc" : "absenceReasonAsc";
            ViewData["StartDateSortParam"] = sortOrder == "startDateAsc" ? "startDateDesc" : "startDateAsc";
            ViewData["EndDateSortParam"] = sortOrder == "endDateAsc" ? "endDateDesc" : "endDateAsc";
            ViewData["StatusSortParam"] = sortOrder == "statusAsc" ? "statusDesc" : "statusAsc";

            ViewData["CurrentSearchPhrase"] = searchPhrase;
            ViewData["CurrentSortOrder"] = sortOrder;

            var allLeaveRequests = await _leaveRequestService.GetAllLeaveRequestsAsync(searchPhrase, sortOrder);

            return View(allLeaveRequests);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createLeaveRequestDto = _leaveRequestService.GetCreateLeaveRequest();

            return View(createLeaveRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveRequestDto createLeaveRequestDto)
        {
            
            if (!ModelState.IsValid)
            {
                var createLeaveRequestDtoAfterValidation = _leaveRequestService.GetLeaveRequestDtoAfterValidation(createLeaveRequestDto);
                return View(createLeaveRequestDtoAfterValidation);
            }
            
            await _leaveRequestService.CreateLeaveRequestAsync(createLeaveRequestDto);

            this.SetNotification("success", "Leave request added successfully");

            return RedirectToAction(nameof(Create));
        }
    }
}
