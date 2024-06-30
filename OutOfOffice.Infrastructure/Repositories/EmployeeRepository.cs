using Microsoft.EntityFrameworkCore;
using OutOfOffice.Domain.Entities;
using OutOfOffice.Domain.Interfaces;
using OutOfOffice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly OutOfOfficeDbContext _dbContext;

        public EmployeeRepository(OutOfOfficeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetPeoplePartnersAsync()
        {
            var peoplePartners = await _dbContext.Employees
                .Where(e => e.Position == "HR Manager")
                .ToListAsync();

            return peoplePartners;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            if (employee.PeoplePartnerId == 0)
            {
                employee.PeoplePartnerId = null;
            }

            _dbContext.Add(employee);
            await _dbContext.SaveChangesAsync();
        }

        public Task CreateEmployeeProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            var employee = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == email);

            return employee;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync(string searchPhrase, string sortOrder)
        {
            var employees = _dbContext.Employees
                .Include(e => e.PeoplePartner)
                .Include(e => e.EmployeeProjects)
                .ThenInclude(p => p.Project)
                .Where(e => e.Position == "Employee" && (searchPhrase == null || e.FullName.ToLower().Contains(searchPhrase.ToLower())));

            switch (sortOrder)
            {
                case "fullNameDesc":
                    employees = employees.OrderByDescending(e => e.FullName);
                    break;
                case "emailAsc":
                    employees = employees.OrderBy(e => e.Email);
                    break;
                case "emailDesc":
                    employees = employees.OrderByDescending(e => e.Email);
                    break;
                case "subdivisionAsc":
                    employees = employees.OrderBy(e => e.Subdivision);
                    break;
                case "subdivisionDesc":
                    employees = employees.OrderByDescending(e => e.Subdivision);
                    break;
                case "positionAsc":
                    employees = employees.OrderBy(e => e.Position);
                    break;
                case "positionDesc":
                    employees = employees.OrderByDescending(e => e.Position);
                    break;      
                case "statusActive":
                    employees = employees.OrderBy(e => e.Status);
                    break;
                case "statusInactive":
                    employees = employees.OrderByDescending(e => e.Status);
                    break;
                case "peoplePartnerAsc":
                    employees = employees.OrderBy(e => e.PeoplePartner != null ? e.PeoplePartner.FullName : String.Empty);
                    break;
                case "peoplePartnerDesc":
                    employees = employees.OrderByDescending(e => e.PeoplePartner != null ? e.PeoplePartner.FullName : String.Empty);
                    break;
                case "projectTypeAsc":
                    employees = employees.OrderBy(e => e.EmployeeProjects.Any() ? e.EmployeeProjects.First().Project.ProjectType : String.Empty);
                    break;
                case "projectTypeDesc":
                    employees = employees.OrderByDescending(e => e.EmployeeProjects.Any() ? e.EmployeeProjects.First().Project.ProjectType : String.Empty);
                    break;
                case "daysOffAsc":
                    employees = employees.OrderBy(e => e.OutOfOfficeBalance);
                    break;
                case "daysOffDesc":
                    employees = employees.OrderByDescending(e => e.OutOfOfficeBalance);
                    break;
                default:
                    employees = employees.OrderBy(e => e.FullName);
                    break;
            }

            return await employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _dbContext.Employees
                .Include(e => e.EmployeeProjects)
                .ThenInclude(ep => ep.Project)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                throw new InvalidOperationException("Employee with specified id doesn't exist.");
            }

            return employee;
        }

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();

        public async Task<IEnumerable<Employee>> GetProjectManagersAsync()
        {
            var projectManagers = await _dbContext.Employees
                .Where(e => e.Position == "Project Manager")
                .ToListAsync();

            return projectManagers;
        }
    }
}
