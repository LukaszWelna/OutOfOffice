using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Application.Services;

namespace OutOfOffice.MVC.Controllers
{
    public class ApprovalRequestController : Controller
    {
        private readonly IApprovalRequestService _approvalRequestService;

        public ApprovalRequestController(IApprovalRequestService approvalRequestService)
        {
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

            var approvalRequests = await _approvalRequestService.GetAllApprovalRequestsAsync();

            return View(approvalRequests);
        }
    }
}
