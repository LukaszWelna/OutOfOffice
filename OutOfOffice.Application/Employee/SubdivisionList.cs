using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Employee
{
    public static class SubdivisionList
    {
        public static List<Subdivision> Subdivisions { get; set; } = new List<Subdivision>()
        {
            new Subdivision() { Id = 1, Name = "Information Technology (IT)" },
            new Subdivision() { Id = 2, Name = "Human Resources (HR)" },
            new Subdivision() { Id = 3, Name = "Marketing Department" },
            new Subdivision() { Id = 4, Name = "Sales Department" },
        };
    }
}
