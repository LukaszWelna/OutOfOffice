using AutoMapper;
using OutOfOffice.Application.ApplicationUser;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.LeaveRequest;
using OutOfOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, 
            IEmployeeRepository employeeRepository,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _employeeRepository = employeeRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task CreateLeaveRequestAsync(CreateLeaveRequestDto createLeaveRequestDto)
        {
            var leaveRequest = _mapper.Map<OutOfOffice.Domain.Entities.LeaveRequest>(createLeaveRequestDto);
            leaveRequest.AbsenceReason = AbsenceReasonList.AbsenceReasons.FirstOrDefault(a => a.Id == createLeaveRequestDto.AbsenceReasonId)?.Name ?? "";
            var loggedUserEmail = _userContextService.GetCurrentUser().Email;
            var loggedEmployee = await _employeeRepository.GetEmployeeByEmailAsync(loggedUserEmail);

            if (loggedEmployee == null)
            {
                throw new InvalidOperationException("Invalid user id.");
            }

            leaveRequest.EmployeeId = loggedEmployee.Id;
            
            await _leaveRequestRepository.CreateLeaveRequestAsync(leaveRequest);
        }

        public CreateLeaveRequestDto GetCreateLeaveRequest()
        {
            var createLeaveRequestDto = new CreateLeaveRequestDto()
            {
                AbsenceReasons = AbsenceReasonList.AbsenceReasons,
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            return createLeaveRequestDto;
        }

        public CreateLeaveRequestDto GetLeaveRequestDtoAfterValidation(CreateLeaveRequestDto createLeaveRequestDto)
        {
            createLeaveRequestDto.AbsenceReasons = AbsenceReasonList.AbsenceReasons;

            return createLeaveRequestDto;
        }
    }
}
