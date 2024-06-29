using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, 
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
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

            var projectDtos = _mapper.Map<List<GetProjectDto>>(projects);

            return projectDtos;
        }
    }
}
