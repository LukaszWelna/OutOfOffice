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
        public IActionResult Index()
        {
            return View();
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
