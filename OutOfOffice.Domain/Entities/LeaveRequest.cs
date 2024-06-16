using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Entities
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;
        public string AbsenceReason { get; set; } = default!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Comment { get; set; }
        public string Status { get; set; } = default!;
        public ApprovalRequest ApprovalRequest { get; set; } = default!;
    }
}
