using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutOfOffice.Application.LeaveRequest;

namespace OutOfOffice.Application.Services
{
    public interface ILeaveRequestService
    {
        public Task CreateLeaveRequestAsync(CreateLeaveRequestDto createLeaveRequestDto);
        public CreateLeaveRequestDto GetCreateLeaveRequest();
        public CreateLeaveRequestDto GetLeaveRequestDtoAfterValidation(CreateLeaveRequestDto createLeaveRequestDto);
    }
}
