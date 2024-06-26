using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.LeaveRequest
{
    public class GetLeaveRequestDto
    {
        public int Id { get; set; }
        public string Employee { get; set; } = default!;
        public string AbsenceReason { get; set; } = default!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Comment { get; set; }
        public string Status { get; set; } = default!;
    }
}
