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
        public Task<List<Project>> GetAllProjectsAsync(int searchPhrase, string sortOrder);
        public Task<Project> GetProjectByIdAsync(int id);
        public Task Commit();
        public Task<List<Project>> GetProjectManagerProjects(int id);
        public Task DeleteEmployeeProjectById(int id);
        public Task CreateEmployeeProjectAsync(EmployeeProject employeeProject);
    }
}
