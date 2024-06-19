using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Employee
{
    public static class PositionList
    {
        public static List<Position> Positions { get; set; } = new List<Position>()
        {
            new Position() { Id = 1, Name = "Employee" },
            new Position() { Id = 2, Name = "HR Manager" },
            new Position() { Id = 3, Name = "Project Manager" }
        };
    }
}
