using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.ApprovalRequest
{
    public class EditApprovalRequestDto
    {
        public int Id { get; set; }
        public int LeaveRequestId { get; set; }
        public int StatusId { get; set; } = default!;
        public List<ApprovalRequestStatus> Statuses { get; set; } = new List<ApprovalRequestStatus>();
        public string? Comment { get; set; }
    }
}
