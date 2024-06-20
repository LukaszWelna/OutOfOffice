using OutOfOffice.Application.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Services
{
    public interface IEmployeeService
    {
        public Task<CreateEmployeeDto> GetCreateEmployeeDtoAsync();
        public Task<CreateEmployeeDto> GetCreateEmployeeDtoAfterValidationAsync(CreateEmployeeDto createEmployeeDto);
        public Task CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        public Task CreateEmployeeProjectAsync(int projectId);
        public Task<OutOfOffice.Domain.Entities.Employee?> GetEmployeeByEmailAsync(string email);
    }
}
