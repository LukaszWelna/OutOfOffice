using AutoMapper;
using OutOfOffice.Application.ApplicationUser;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.Project;
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
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IProjectRepository projectRepository,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _userContextService = userContextService;
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

        public Task<OutOfOffice.Domain.Entities.Employee?> GetEmployeeByEmailAsync(string email)
        {
            var employee = _employeeRepository.GetEmployeeByEmailAsync(email);

            return employee;
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployeesAsync(string searchPhrase, string sortOrder)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync(searchPhrase, sortOrder);

            var getEmployeeDtos = _mapper.Map<List<GetEmployeeDto>>(employees);

            return getEmployeeDtos;
        }

        public async Task<EditEmployeeDto> GetEditEmployeeDtoByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            var editEmployeeDto = _mapper.Map<EditEmployeeDto>(employee);

            var peoplePartners = await _employeeRepository.GetPeoplePartnersAsync();

            var loggedUser = _userContextService.GetCurrentUser();

            var employeeLogged = await _employeeRepository.GetEmployeeByEmailAsync(loggedUser.Email);

            if (employeeLogged == null)
            {
                throw new InvalidOperationException("Invalid user email.");
            }

            var projects = new List<OutOfOffice.Domain.Entities.Project>();

            if (employeeLogged.Position == "Project Manager")
            {
                projects = await _projectRepository.GetProjectManagerProjects(employeeLogged.Id);
            }

            var employeeProject = employee.EmployeeProjects
                .FirstOrDefault(ep => ep.EmployeeId == employee.Id);

            if (employeeProject != null)
            {
                editEmployeeDto.ProjectId = employeeProject.ProjectId;
            }
            else
            {
                editEmployeeDto.ProjectId = 0;
            }

            editEmployeeDto.Subdivisions = SubdivisionList.Subdivisions;
            editEmployeeDto.Positions = PositionList.Positions;
            editEmployeeDto.PeoplePartners = peoplePartners.ToList();
            editEmployeeDto.Projects = projects;

            return editEmployeeDto;
        }

        public async Task EditEmployeeById(EditEmployeeDto editEmployeeDto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(editEmployeeDto.Id);

            employee.FullName = editEmployeeDto.FullName!;
            employee.Subdivision = SubdivisionList.Subdivisions.FirstOrDefault(s => s.Id == editEmployeeDto.SubdivisionId)?.Name ?? "";
            employee.Position = PositionList.Positions.FirstOrDefault(p => p.Id == editEmployeeDto.PositionId)?.Name ?? "";
            employee.Status = editEmployeeDto.Status;
            employee.PeoplePartnerId = editEmployeeDto.PeoplePartnerId != 0 ? editEmployeeDto.PeoplePartnerId : null;
            employee.OutOfOfficeBalance = editEmployeeDto.OutOfOfficeBalance;
            employee.Photo = editEmployeeDto.Photo;

            var employeeProject = employee.EmployeeProjects
                .FirstOrDefault(ep => ep.EmployeeId == employee.Id);

            if (employeeProject != null)
            {
                if (editEmployeeDto.ProjectId != 0)
                {
                    employeeProject.ProjectId = editEmployeeDto.ProjectId!.Value;
                }
                else
                {
                    await _projectRepository.DeleteEmployeeProjectById(employeeProject.Id);
                }
            }
            else
            {
                if (editEmployeeDto.ProjectId != 0)
                {
                    var newEmployeeProject = new EmployeeProject()
                    {
                        EmployeeId = employee.Id,
                        ProjectId = editEmployeeDto.ProjectId!.Value
                    };

                    await _projectRepository.CreateEmployeeProjectAsync(newEmployeeProject);
                }
            }

            await _employeeRepository.Commit();
        }

        public async Task<EditEmployeeDto> GetEditEmployeeDtoAfterValidationAsync(EditEmployeeDto editEmployeeDto)
        {
            var peoplePartners = await _employeeRepository.GetPeoplePartnersAsync();

            editEmployeeDto.Subdivisions = SubdivisionList.Subdivisions;
            editEmployeeDto.Positions = PositionList.Positions;
            editEmployeeDto.PeoplePartners = peoplePartners.ToList();

            return editEmployeeDto;
        }
    }
}
