using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Entities
{
    public class EmployeeProject
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = default!;
    }
}
