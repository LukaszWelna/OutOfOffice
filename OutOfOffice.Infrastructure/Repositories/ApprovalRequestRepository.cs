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

        public async Task<List<ApprovalRequest>> GetAllApprovalRequestsAsync()
        {
            var loggedUser = _userContextService.GetCurrentUser();

            var approvalRequests = _dbContext.ApprovalRequests
            .Include(a => a.LeaveRequest)
            .ThenInclude(l => l.Employee)
            .ThenInclude(e => e.EmployeeProjects)
            .ThenInclude(ep => ep.Project)
            .AsQueryable();

            if (loggedUser.Role == "HR Manager")
            {
                approvalRequests = approvalRequests.Where(a => a.LeaveRequest.Employee.PeoplePartner != null &&
                    a.LeaveRequest.Employee.PeoplePartner.Email == loggedUser.Email);
            }

            return await approvalRequests.ToListAsync();
        }
    }
}
