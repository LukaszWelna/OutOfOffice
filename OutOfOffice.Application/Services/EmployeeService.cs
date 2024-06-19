using OutOfOffice.Application.Employee;
using OutOfOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<CreateEmployeeDto> GetCreateEmployeeDto()
        {
            var peoplePartners = await _employeeRepository.GetPeoplePartners();

            var createEmployeeDto = new CreateEmployeeDto()
            {
                Subdivisions = SubdivisionList.Subdivisions,
                Positions = PositionList.Positions,
                PeoplePartners = peoplePartners.ToList()
            };

            return createEmployeeDto;
        }
    }
}
