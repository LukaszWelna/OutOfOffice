using Microsoft.Data.SqlClient;
using OutOfOffice.Application.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Services
{
    public interface IProjectService
    {
        public Task<CreateProjectDto> GetCreateProjectDtoAsync();
        public Task<CreateProjectDto> GetCreateProjectDtoAfterValidation(CreateProjectDto createProjectDto);
        public Task CreateProjectAsync(CreateProjectDto createProjectDto);
        public Task<List<GetProjectDto>> GetAllProjectsAsync(int searchPhrase, string sortOrder);
    }
}
