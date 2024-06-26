using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.LeaveRequest
{
    public class EditLeaveRequestDto
    {
        public int Id { get; set; }
        public int AbsenceReasonId { get; set; }
        public List<AbsenceReason> AbsenceReasons { get; set; } = new List<AbsenceReason>();
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Comment { get; set; }
    }
}
