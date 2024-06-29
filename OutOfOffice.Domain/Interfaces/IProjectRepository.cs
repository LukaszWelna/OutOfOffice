using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Interfaces
{
    public interface IProjectRepository
    {
        public Task CreateProjectAsync(Project project, EmployeeProject employeeProject);
    }
}
