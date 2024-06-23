using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Employee
{
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Subdivision { get; set; } = default!;
        public string Position { get; set; } = default!;
        public bool Status { get; set; }
        public string? PeoplePartner { get; set; }
        public int OutOfOfficeBalance { get; set; }
        public string? ProjectType { get; set; }
    }
}
