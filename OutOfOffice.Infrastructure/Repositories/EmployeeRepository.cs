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
    }
}
