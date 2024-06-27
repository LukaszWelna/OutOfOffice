using Microsoft.EntityFrameworkCore;
using OutOfOffice.Application.ApplicationUser;
using OutOfOffice.Application.Services;
using OutOfOffice.Domain.Entities;
using OutOfOffice.Domain.Interfaces;
using OutOfOffice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Infrastructure.Repositories
{
    public class ApprovalRequestRepository : IApprovalRequestRepository
    {
        private readonly OutOfOfficeDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public ApprovalRequestRepository(OutOfOfficeDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public async Task CreateApprovalRequestAsync(ApprovalRequest approvalRequest)
        {
            _dbContext.Add(approvalRequest);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteApprovalRequestAsync(int leaveRequestId)
        {
            var approvalRequest = await _dbContext.ApprovalRequests
                .FirstOrDefaultAsync(a => a.LeaveRequestId == leaveRequestId);

            if (approvalRequest == null)
            {
                throw new InvalidOperationException("Approval request with specified leave request id doesn't exist.");
            }

            _dbContext.ApprovalRequests.Remove(approvalRequest);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ApprovalRequest>> GetAllApprovalRequestsAsync(int searchPhrase, string sortOrder)
        {
            var loggedUser = _userContextService.GetCurrentUser();

            var approvalRequests = _dbContext.ApprovalRequests
                .Include(a => a.Approver)
                .Include(a => a.LeaveRequest)
                .ThenInclude(l => l.Employee)
                .ThenInclude(e => e.EmployeeProjects)
                .ThenInclude(ep => ep.Project)
                .AsQueryable();

            if (loggedUser.Role == "HR Manager")
            {
                approvalRequests = approvalRequests.Where(a => a.LeaveRequest.Employee.PeoplePartner != null &&
                    a.LeaveRequest.Employee.PeoplePartner.Email == loggedUser.Email
                    && searchPhrase == 0 || a.LeaveRequestId == searchPhrase);
            }

            switch (sortOrder)
            {
                case "leaveRequestIdDesc":
                    approvalRequests = approvalRequests.OrderByDescending(a => a.LeaveRequestId);
                    break;
                case "approverAsc":
                    approvalRequests = approvalRequests.OrderBy(a => a.Approver!.FullName);
                    break;
                case "approverDesc":
                    approvalRequests = approvalRequests.OrderByDescending(a => a.Approver!.FullName);
                    break;
                case "statusAsc":
                    approvalRequests = approvalRequests.OrderBy(a => a.Status);
                    break;
                case "statusDesc":
                    approvalRequests = approvalRequests.OrderByDescending(a => a.Status);
                    break;
                default:
                    approvalRequests = approvalRequests.OrderBy(a => a.LeaveRequestId);
                    break;
            }

            return await approvalRequests.ToListAsync();
        }

        public async Task<ApprovalRequest> GetApprovalRequestByIdAsync(int id)
        {
            var approvalRequest = await _dbContext.ApprovalRequests
                .FirstOrDefaultAsync(a => a.Id == id);

            if (approvalRequest == null)
            {
                throw new InvalidOperationException("Approval request with specified id doesn't exist.");
            }

            return approvalRequest;
        }

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();
    }
}
