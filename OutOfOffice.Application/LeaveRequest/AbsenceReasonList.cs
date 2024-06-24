using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.LeaveRequest
{
    public static class AbsenceReasonList
    {
        public static List<AbsenceReason> AbsenceReasons { get; set; } = new List<AbsenceReason>()
        {
            new AbsenceReason() { Id = 1, Name = "Illness" },
            new AbsenceReason() { Id = 2, Name = "Vacation" },
            new AbsenceReason() { Id = 3, Name = "Child care" },
            new AbsenceReason() { Id = 3, Name = "Other" }
        };
    }
}
