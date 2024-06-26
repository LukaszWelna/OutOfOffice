using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.ApprovalRequest
{
    public class CreateApprovalRequestDto
    {
        public int Id { get; set; }
        public int LeaveRequestId { get; set; }
        public string Status { get; set; } = default!;
        public string? Comment { get; set; }
    }
}
