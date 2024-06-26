using Microsoft.EntityFrameworkCore;
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

        public ApprovalRequestRepository(OutOfOfficeDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
