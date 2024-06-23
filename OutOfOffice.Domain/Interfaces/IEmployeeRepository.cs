using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetPeoplePartnersAsync();
        public Task CreateEmployeeAsync(Employee employee);
        public Task CreateEmployeeProjectAsync(int projectId);
        public Task<Employee?> GetEmployeeByEmailAsync(string email);
        public Task<List<Employee>> GetAllEmployeesAsync(string searchPhrase, string sortOrder);
        public Task<Employee> GetEmployeeByIdAsync(int id);
        public Task Commit();
    }
}
