using AutoMapper;
using OutOfOffice.Application.ApplicationUser;
using OutOfOffice.Application.ApprovalRequest;
using OutOfOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Services
{
    public class ApprovalRequestService : IApprovalRequestService
    {
        private readonly IApprovalRequestRepository _approvalRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public ApprovalRequestService(IApprovalRequestRepository approvalRequestRepository,
            IEmployeeRepository employeeRepository,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _approvalRequestRepository = approvalRequestRepository;
            _employeeRepository = employeeRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task CreateApprovalRequestAsync(int leaveRequestId)
        {
            var approvalRequest = new OutOfOffice.Domain.Entities.ApprovalRequest();

            approvalRequest.LeaveRequestId = leaveRequestId;
            approvalRequest.Status = "New";
            
            await _approvalRequestRepository.CreateApprovalRequestAsync(approvalRequest);
        }

        public async Task DeleteApprovalRequestAsync(int leaveRequestId)
        {
            await _approvalRequestRepository.DeleteApprovalRequestAsync(leaveRequestId);
        }

        public async Task<List<GetApprovalRequestDto>> GetAllApprovalRequestsAsync()
        {
            var approvalRequests = await _approvalRequestRepository.GetAllApprovalRequestsAsync();

            var approvalRequestDtos = _mapper.Map<List<GetApprovalRequestDto>>(approvalRequests);

            return approvalRequestDtos;
        }
    }
}
