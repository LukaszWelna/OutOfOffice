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
    }
}
