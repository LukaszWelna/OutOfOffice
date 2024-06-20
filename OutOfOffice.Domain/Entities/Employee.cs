using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Subdivision { get; set; } = default!;
        public string Position { get; set; } = default!;
        public bool Status { get; set; }
        public int? PeoplePartnerId { get; set; }
        public Employee? PeoplePartner { get; set; }
        public int OutOfOfficeBalance { get; set; }
        public byte[]? Photo { get; set; }
        public ApprovalRequest ApprovalRequest { get; set; } = default!;
        public LeaveRequest LeaveRequest { get; set; } = default!;
        public List<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
