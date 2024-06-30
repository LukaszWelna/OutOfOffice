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
    public class ProjectRepository : IProjectRepository
    {
        private readonly OutOfOfficeDbContext _dbContext;

        public ProjectRepository(OutOfOfficeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProjectAsync(Project project, EmployeeProject employeeProject)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Add(project);
                    await _dbContext.SaveChangesAsync();

                    employeeProject.ProjectId = project.Id;

                    _dbContext.Add(employeeProject);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new InvalidOperationException("An error occured while adding the project.");
                }
            }
        }

        public async Task<List<Project>> GetAllProjectsAsync(int searchPhrase, string sortOrder)
        {
            var projects = _dbContext.Projects
                .Include(p => p.EmployeeProjects)
                .ThenInclude(ep => ep.Employee)
                .Where(p => searchPhrase == 0 || p.Id == searchPhrase);

            switch (sortOrder)
            {
                case "IdDesc":
                    projects = projects.OrderByDescending(p => p.Id);
                    break;
                case "projectTypeAsc":
                    projects = projects.OrderBy(p => p.ProjectType);
                    break;
                case "projectTypeDesc":
                    projects = projects.OrderByDescending(p => p.ProjectType);
                    break;
                case "startDateAsc":
                    projects = projects.OrderBy(p => p.StartDate);
                    break;
                case "startDateDesc":
                    projects = projects.OrderByDescending(p => p.StartDate);
                    break;
                case "endDateAsc":
                    projects = projects.OrderBy(p => p.EndDate);
                    break;
                case "endDateDesc":
                    projects = projects.OrderByDescending(p => p.EndDate);
                    break;
                case "projectManagerAsc":
                    projects = projects.OrderBy(p => p.EmployeeProjects
                        .Where(ep => ep.Employee.Position == "Project Manager")
                        .Select(ep => ep.Employee.FullName)
                        .FirstOrDefault());
                    break;
                case "projectManagerDesc":
                    projects = projects.OrderByDescending(p => p.EmployeeProjects
                        .Where(ep => ep.Employee.Position == "Project Manager")
                        .Select(ep => ep.Employee.FullName)
                        .FirstOrDefault());
                    break;
                case "statusAsc":
                    projects = projects.OrderBy(p => p.Status);
                    break;
                case "statusDesc":
                    projects = projects.OrderByDescending(p => p.Status);
                    break;
                default:
                    projects = projects.OrderBy(p => p.Id);
                    break;
            }

            return await projects.ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            var project = await _dbContext.Projects
                .Include(p => p.EmployeeProjects)
                .ThenInclude(ep => ep.Employee)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                throw new InvalidOperationException("Project with specified id doesn't exist.");
            }

            return project;
        }

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();

        public async Task<List<Project>> GetProjectManagerProjects(int id)
        {
           var projects = await _dbContext.Projects
                .Include(p => p.EmployeeProjects)
                .Where(p => p.EmployeeProjects.Any(ep => ep.EmployeeId == id))
                .ToListAsync();

            return projects;
        }

        public async Task DeleteEmployeeProjectById(int id)
        {
            var employeeProject = await _dbContext.EmployeeProjects
                .FirstOrDefaultAsync(ep => ep.Id == id);

            if (employeeProject == null)
            {
                throw new InvalidOperationException("Employee project with specified id doesn't exist.");
            }

            _dbContext.EmployeeProjects.Remove(employeeProject);

            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateEmployeeProjectAsync(EmployeeProject employeeProject)
        {
            _dbContext.Add(employeeProject);

            await _dbContext.SaveChangesAsync();
        }
    }
}
