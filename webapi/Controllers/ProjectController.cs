using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using webapi.Models;
using webapi.Services;
using webapi.Identity;

namespace webapi.Controllers;

[Authorize] // requires authorization for the entire controller, can also be put indiviually on each route
[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase {
    ProjectService _projectservice;
    
    public ProjectController(ProjectService service) {
        _projectservice = service;
    }

    [AllowAnonymous] // overwrites authorization for this route only
    [HttpGet]
    public IEnumerable<Project> GetAll() {
        return _projectservice.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Project> GetById(int id) {
        Project? project = _projectservice.GetById(id);

        if (project != null) {
            return project;
        } else {
            return NotFound();
        }
    }

    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [HttpPost]
    public IActionResult Create(Project new_project) {
        return null;
    }
}