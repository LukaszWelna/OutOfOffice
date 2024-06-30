using Microsoft.AspNetCore.Authorization;
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
        private readonly IApprovalRequestService _approvalRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, 
            IApprovalRequestService approvalRequestService)
        {
            _leaveRequestService = leaveRequestService;
            _approvalRequestService = approvalRequestService;
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

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public IActionResult Create()
        {
            var createLeaveRequestDto = _leaveRequestService.GetCreateLeaveRequest();

            return View(createLeaveRequestDto);
        }

        [Authorize(Roles = "Employee")]
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

        [Authorize(Roles = "Employee, Administrator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editLeaveRequestDto = await _leaveRequestService.GetEditLeaveRequestDtoByIdAsync(id);

            return View(editLeaveRequestDto);
        }

        [Authorize(Roles = "Employee, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditLeaveRequestDto editLeaveRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var editLeaveRequestDtoAfterValidation = _leaveRequestService.GetEditLeaveRequestDtoAfterValidation(editLeaveRequestDto);
                
                return View(editLeaveRequestDtoAfterValidation);
            }

            await _leaveRequestService.EditLeaveRequestById(editLeaveRequestDto);

            this.SetNotification("success", "Leave request edited successfully");

            return RedirectToAction("Index", "LeaveRequest");
        }

        [Authorize(Roles = "Employee, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Submit(int id)
        {
            await _leaveRequestService.SubmitLeaveRequestByIdAsync(id);

            await _approvalRequestService.CreateApprovalRequestAsync(id);

            this.SetNotification("success", "Leave request submitted successfully");

            return RedirectToAction("Index", "LeaveRequest");
        }

        [Authorize(Roles = "Employee, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            await _leaveRequestService.CancelLeaveRequestByIdAsync(id);

            await _approvalRequestService.DeleteApprovalRequestAsync(id);

            this.SetNotification("success", "Leave request cancelled successfully");

            return RedirectToAction("Index", "LeaveRequest");
        }
    }
}
