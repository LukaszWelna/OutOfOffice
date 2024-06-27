using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.ApprovalRequest
{
    public static class ApprovalRequestStatusList
    {
        public static List<ApprovalRequestStatus> ApprovalRequestStatuses { get; set; } = new List<ApprovalRequestStatus>()
        {
            new ApprovalRequestStatus() { Id = 1, Name = "Approved" },
            new ApprovalRequestStatus() { Id = 2, Name = "Rejected" }
        };
    }
}
