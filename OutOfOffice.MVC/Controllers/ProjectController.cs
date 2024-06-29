using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Application.LeaveRequest;
using OutOfOffice.Application.Project;
using OutOfOffice.Application.Services;
using OutOfOffice.MVC.Extensions;

namespace OutOfOffice.MVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allProjects = await _projectService.GetAllProjectsAsync();

            return View(allProjects);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var createProjectDto = await _projectService.GetCreateProjectDtoAsync();

            return View(createProjectDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto createProjectDto)
        {
            if (!ModelState.IsValid)
            {
                var createProjectDtoAfterValidation = await _projectService.GetCreateProjectDtoAfterValidation(createProjectDto);

                return View(createProjectDtoAfterValidation);
            }
            
            await _projectService.CreateProjectAsync(createProjectDto);

            this.SetNotification("success", "Project added successfully");
            
            return RedirectToAction(nameof(Create));
        }
    }
}
