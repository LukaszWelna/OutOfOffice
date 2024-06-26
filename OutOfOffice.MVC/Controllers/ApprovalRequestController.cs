﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Application.ApprovalRequest;
using OutOfOffice.Application.LeaveRequest;
using OutOfOffice.Application.Services;
using OutOfOffice.MVC.Extensions;

namespace OutOfOffice.MVC.Controllers
{
    public class ApprovalRequestController : Controller
    {
        private readonly IApprovalRequestService _approvalRequestService;

        public ApprovalRequestController(IApprovalRequestService approvalRequestService)
        {
            _approvalRequestService = approvalRequestService;
        }

        // Show approval requests
        [Authorize(Roles = "HR Manager, Project Manager, Administrator")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int searchPhrase, [FromQuery] string sortOrder)
        {
            ViewData["LeaveRequestIdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "leaveRequestIdDesc" : "";
            ViewData["ApproverSortParam"] = sortOrder == "approverAsc" ? "approverDesc" : "approverAsc";
            ViewData["StatusSortParam"] = sortOrder == "statusAsc" ? "statusDesc" : "statusAsc";

            ViewData["CurrentSearchPhrase"] = searchPhrase;
            ViewData["CurrentSortOrder"] = sortOrder;

            var approvalRequests = await _approvalRequestService.GetAllApprovalRequestsAsync(searchPhrase, sortOrder);

            return View(approvalRequests);
        }

        // Manage editing approval requests
        [Authorize(Roles = "HR Manager, Project Manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editApprovalRequestDto = await _approvalRequestService.GetEditApprovalRequestDtoByIdAsync(id);

            return View(editApprovalRequestDto);
        }

        [Authorize(Roles = "HR Manager, Project Manager")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditApprovalRequestDto editApprovalRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var editApprovalRequestDtoAfterValidation = _approvalRequestService.GetEditApprovalRequestDtoAfterValidation(editApprovalRequestDto);

                return View(editApprovalRequestDto);
            }
            
            await _approvalRequestService.EditApprovalRequestById(editApprovalRequestDto);

            this.SetNotification("success", "Approval request edited successfully");

            return RedirectToAction("Index", "ApprovalRequest");
        }
    }
}
