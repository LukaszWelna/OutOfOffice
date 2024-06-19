using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Employee
{
    public class CreateEmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public int SubdivisionId { get; set; }
        public List<Subdivision> Subdivisions { get; set; } = new List<Subdivision>();
        public int PositionId { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
        public bool Status { get; set; }
        public int PeoplePartnerId { get; set; }
        public List<OutOfOffice.Domain.Entities.Employee> PeoplePartners { get; set; } = new List<OutOfOffice.Domain.Entities.Employee>();
        public int OutOfOfficeBalance { get; set; }
        public byte[]? Photo { get; set; }
    }
}
