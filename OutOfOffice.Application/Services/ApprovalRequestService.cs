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
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IUserContextService _userContextService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public ApprovalRequestService(IApprovalRequestRepository approvalRequestRepository,
            ILeaveRequestRepository leaveRequestRepository,
            IUserContextService userContextService,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _approvalRequestRepository = approvalRequestRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _userContextService = userContextService;
            _employeeRepository = employeeRepository;
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

        public async Task<List<GetApprovalRequestDto>> GetAllApprovalRequestsAsync(int searchPhrase, string sortOrder)
        {
            var approvalRequests = await _approvalRequestRepository.GetAllApprovalRequestsAsync(searchPhrase, sortOrder);
            
            var approvalRequestDtos = _mapper.Map<List<GetApprovalRequestDto>>(approvalRequests);

            return approvalRequestDtos;
        }

        public async Task<EditApprovalRequestDto> GetEditApprovalRequestDtoByIdAsync(int id)
        {
            var approvalRequest = await _approvalRequestRepository.GetApprovalRequestByIdAsync(id);

            var editApprovalRequestDto = _mapper.Map<EditApprovalRequestDto>(approvalRequest);

            return editApprovalRequestDto;
        }

        public async Task EditApprovalRequestById(EditApprovalRequestDto editApprovalRequestDto)
        {
            var approvalRequest = await _approvalRequestRepository.GetApprovalRequestByIdAsync(editApprovalRequestDto.Id);

            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(approvalRequest.LeaveRequestId);

            var loggedUser = _userContextService.GetCurrentUser();

            var employee = await _employeeRepository.GetEmployeeByEmailAsync(loggedUser.Email);

            if (employee == null)
            {
                throw new InvalidOperationException("Invalid user id.");
            }
            
            approvalRequest.ApproverId = employee.Id;
            approvalRequest.Status = ApprovalRequestStatusList.ApprovalRequestStatuses.FirstOrDefault(s => s.Id == editApprovalRequestDto.StatusId)!.Name;
            approvalRequest.Comment = editApprovalRequestDto.Comment;

            leaveRequest.Status = approvalRequest.Status;
            leaveRequest.Comment = approvalRequest.Comment;

            await _approvalRequestRepository.Commit();
        }

        public EditApprovalRequestDto GetEditApprovalRequestDtoAfterValidation(EditApprovalRequestDto editApprovalRequestDto)
        {
            editApprovalRequestDto.Statuses = ApprovalRequestStatusList.ApprovalRequestStatuses;

            return editApprovalRequestDto;
        }
    }
}
