using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.LeaveRequest;

namespace OutOfOffice.Application.Services
{
    public interface ILeaveRequestService
    {
        public Task CreateLeaveRequestAsync(CreateLeaveRequestDto createLeaveRequestDto);
        public CreateLeaveRequestDto GetCreateLeaveRequest();
        public CreateLeaveRequestDto GetLeaveRequestDtoAfterValidation(CreateLeaveRequestDto createLeaveRequestDto);
        public Task<List<GetLeaveRequestDto>> GetAllLeaveRequestsAsync(string searchPhrase, string sortOrder);
        public Task<EditLeaveRequestDto> GetEditLeaveRequestDtoByIdAsync(int id);
        public Task EditLeaveRequestById(EditLeaveRequestDto editLeaveRequestDto);
        public EditLeaveRequestDto GetEditLeaveRequestDtoAfterValidation(EditLeaveRequestDto editLeaveRequestDto);
        public Task SubmitLeaveRequestByIdAsync(int id);
        public Task CancelLeaveRequestByIdAsync(int id);
    }
}
