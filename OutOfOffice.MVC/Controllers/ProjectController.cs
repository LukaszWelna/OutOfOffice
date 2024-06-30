using Microsoft.AspNetCore.Authorization;
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

        // Show all projects
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int searchPhrase, [FromQuery] string sortOrder)
        {
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "IdDesc" : "";
            ViewData["ProjectTypeSortParam"] = sortOrder == "projectTypeAsc" ? "projectTypeDesc" : "projectTypeAsc";
            ViewData["StartDateSortParam"] = sortOrder == "startDateAsc" ? "startDateDesc" : "startDateAsc";
            ViewData["EndDateSortParam"] = sortOrder == "endDateAsc" ? "endDateDesc" : "endDateAsc";
            ViewData["ProjectManagerSortParam"] = sortOrder == "projectManagerAsc" ? "projectManagerDesc" : "projectManagerAsc";
            ViewData["StatusSortParam"] = sortOrder == "statusAsc" ? "statusDesc" : "statusAsc";

            ViewData["CurrentSearchPhrase"] = searchPhrase;
            ViewData["CurrentSortOrder"] = sortOrder;

            var allProjects = await _projectService.GetAllProjectsAsync(searchPhrase, sortOrder);

            return View(allProjects);
        }

        // Manage creating a new project
        [Authorize(Roles = "Project Manager, Administrator")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var createProjectDto = await _projectService.GetCreateProjectDtoAsync();

            return View(createProjectDto);
        }

        [Authorize(Roles = "Project Manager, Administrator")]
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

        // Manage editing of project
        [Authorize(Roles = "Project Manager, Administrator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editProjectDto = await _projectService.GetEditProjectDtoByIdAsync(id);

            return View(editProjectDto);
        }

        [Authorize(Roles = "Project Manager, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectDto editProjectDto)
        {
            if (!ModelState.IsValid)
            {
                var editProjectDtoAfterValidation = await _projectService.GetEditProjectDtoAfterValidation(editProjectDto);

                return View(editProjectDtoAfterValidation);
            }

            await _projectService.EditProjectById(editProjectDto);

            this.SetNotification("success", "Project edited successfully");

            return RedirectToAction("Index", "Project");
        }
    }
}
