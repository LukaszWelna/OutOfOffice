using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Interfaces
{
    public interface IApprovalRequestRepository
    {
        public Task CreateApprovalRequestAsync(ApprovalRequest approvalRequest);
        public Task DeleteApprovalRequestAsync(int leaveRequestId);
        public Task<List<OutOfOffice.Domain.Entities.ApprovalRequest>> GetAllApprovalRequestsAsync();
    }
}
