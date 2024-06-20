using AutoMapper;
using OutOfOffice.Application.Employee;
using OutOfOffice.Domain.Entities;
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
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<CreateEmployeeDto> GetCreateEmployeeDtoAsync()
        {
            var peoplePartners = await _employeeRepository.GetPeoplePartnersAsync();

            var createEmployeeDto = new CreateEmployeeDto()
            {
                Subdivisions = SubdivisionList.Subdivisions,
                Positions = PositionList.Positions,
                PeoplePartners = peoplePartners.ToList()
            };

            return createEmployeeDto;
        }

        public async Task<CreateEmployeeDto> GetCreateEmployeeDtoAfterValidationAsync(CreateEmployeeDto createEmployeeDto)
        {
            var peoplePartners = await _employeeRepository.GetPeoplePartnersAsync();

            createEmployeeDto.Subdivisions = SubdivisionList.Subdivisions;
            createEmployeeDto.Positions = PositionList.Positions;
            createEmployeeDto.PeoplePartners = peoplePartners.ToList();

            return createEmployeeDto;
        }

        public async Task CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = _mapper.Map<OutOfOffice.Domain.Entities.Employee>(createEmployeeDto);
            employee.Subdivision = SubdivisionList.Subdivisions.FirstOrDefault(s => s.Id == createEmployeeDto.SubdivisionId)?.Name ?? "";
            employee.Position = PositionList.Positions.FirstOrDefault(p => p.Id == createEmployeeDto.PositionId)?.Name ?? "";

            await _employeeRepository.CreateEmployeeAsync(employee);
        }

        public Task CreateEmployeeProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<OutOfOffice.Domain.Entities.Employee?> GetEmployeeByEmailAsync(string email)
        {
            var employee = _employeeRepository.GetEmployeeByEmailAsync(email);

            return employee;
        }
    }
}
