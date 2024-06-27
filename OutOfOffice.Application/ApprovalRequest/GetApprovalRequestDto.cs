using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.ApprovalRequest
{
    public class GetApprovalRequestDto
    {
        public int Id { get; set; }
        public int ApproverId { get; set; }
        public string? ApproverName { get; set; }
        public int LeaveRequestId { get; set; }
        public string Status { get; set; } = default!;
        public string? Comment { get; set; }
    }
}
