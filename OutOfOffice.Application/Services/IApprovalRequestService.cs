using OutOfOffice.Application.ApprovalRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Services
{
    public interface IApprovalRequestService
    {
        public Task CreateApprovalRequestAsync(int leaveRequestId);
        public Task DeleteApprovalRequestAsync(int leaveRequestId);
        public Task<List<GetApprovalRequestDto>> GetAllApprovalRequestsAsync(int searchPhrase, string sortOrder);
        public Task<EditApprovalRequestDto> GetEditApprovalRequestDtoByIdAsync(int id);
        public Task EditApprovalRequestById(EditApprovalRequestDto editApprovalRequestDto);
        public EditApprovalRequestDto GetEditApprovalRequestDtoAfterValidation(EditApprovalRequestDto editApprovalRequestDto);
    }
}
