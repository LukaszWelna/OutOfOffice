using AutoMapper;
using OutOfOffice.Application.ApplicationUser;
using OutOfOffice.Application.LeaveRequest;
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
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, 
            IEmployeeRepository employeeRepository,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<CreateProjectDto> GetCreateProjectDtoAsync()
        {
            var projectManagers = await _employeeRepository.GetProjectManagersAsync();

            var createProjectDto = new CreateProjectDto()
            {
                ProjectTypes = ProjectTypeList.ProjectTypes,
                ProjectManagers = projectManagers.ToList(),
                ProjectStatuses = ProjectStatusList.ProjectStatuses,
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            return createProjectDto;
        }

        public async Task<CreateProjectDto> GetCreateProjectDtoAfterValidation(CreateProjectDto createProjectDto)
        {
            var projectManagers = await _employeeRepository.GetProjectManagersAsync();

            createProjectDto.ProjectTypes = ProjectTypeList.ProjectTypes;
            createProjectDto.ProjectManagers = projectManagers.ToList();
            createProjectDto.ProjectStatuses = ProjectStatusList.ProjectStatuses;
            
            return createProjectDto;
        }

        public async Task CreateProjectAsync(CreateProjectDto createProjectDto)
        {
            var project = _mapper.Map<OutOfOffice.Domain.Entities.Project>(createProjectDto);
            project.ProjectType = ProjectTypeList.ProjectTypes.FirstOrDefault(p => p.Id == createProjectDto.ProjectTypeId)!.Name;
            project.Status = ProjectStatusList.ProjectStatuses.FirstOrDefault(p => p.Id == createProjectDto.StatusId)!.Name;

            var employeeProject = new EmployeeProject()
            {
                EmployeeId = createProjectDto.ProjectManagerId,
            };

            await _projectRepository.CreateProjectAsync(project, employeeProject);
        }

        public async Task<List<GetProjectDto>> GetAllProjectsAsync(int searchPhrase, string sortOrder)
        {
            var projects = await _projectRepository.GetAllProjectsAsync(searchPhrase, sortOrder);

            var loggedUserEmail = _userContextService.GetCurrentUser().Email;
            var loggedEmployee = await _employeeRepository.GetEmployeeByEmailAsync(loggedUserEmail);

            if (loggedEmployee == null)
            {
                throw new InvalidOperationException("Invalid user id.");
            }

            if (loggedEmployee.Position == "Employee")
            {
                projects = projects.Where(p => p.EmployeeProjects.Any(ep => ep.EmployeeId == loggedEmployee.Id)).ToList();
            }

            var projectDtos = _mapper.Map<List<GetProjectDto>>(projects);

            return projectDtos;
        }

        public async Task<EditProjectDto> GetEditProjectDtoByIdAsync(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            var projectManagers = await _employeeRepository.GetProjectManagersAsync();

            var projectDto = _mapper.Map<EditProjectDto>(project);

            projectDto.ProjectTypes = ProjectTypeList.ProjectTypes;
            projectDto.ProjectStatuses = ProjectStatusList.ProjectStatuses;
            projectDto.ProjectManagers = projectManagers.ToList();

            return projectDto;
        }

        public async Task<EditProjectDto> GetEditProjectDtoAfterValidation(EditProjectDto editProjectDto)
        {
            var projectManagers = await _employeeRepository.GetProjectManagersAsync();

            editProjectDto.ProjectTypes = ProjectTypeList.ProjectTypes;
            editProjectDto.ProjectStatuses = ProjectStatusList.ProjectStatuses;
            editProjectDto.ProjectManagers = projectManagers.ToList();

            return editProjectDto;
        }

        public async Task EditProjectById(EditProjectDto editProjectDto)
        {
            var project = await _projectRepository.GetProjectByIdAsync(editProjectDto.Id);

            project.ProjectType = ProjectTypeList.ProjectTypes.FirstOrDefault(pt => pt.Id == editProjectDto.ProjectTypeId)?.Name ?? "";
            project.Status = ProjectStatusList.ProjectStatuses.FirstOrDefault(ps => ps.Id == editProjectDto.StatusId)?.Name ?? "";
            project.StartDate = editProjectDto.StartDate;
            project.EndDate = editProjectDto.EndDate;
            project.Comment = editProjectDto.Comment;

            var employeeProject = project.EmployeeProjects
                .FirstOrDefault(ep => ep.ProjectId == project.Id && ep.Employee.Position == "Project Manager");

            if (employeeProject == null)
            {
                throw new InvalidOperationException("EmployeeProject with specified id and project manager doesn't exist.");
            }

            employeeProject.EmployeeId = editProjectDto.ProjectManagerId;

            await _projectRepository.Commit();
        }
    }
}
