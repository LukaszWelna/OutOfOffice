using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Entities
{
    public class ApprovalRequest
    {
        public int Id { get; set; }
        public int ApproverId { get; set; }
        public Employee Approver { get; set; } = default!;
        public int LeaveRequestId { get; set; }
        public LeaveRequest LeaveRequest { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string? Comment { get; set; }
    }
}
