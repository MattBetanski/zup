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

    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [HttpPost]
    public IActionResult Create(Project new_project) {
        return Ok();
    }

    [HttpGet("{project_id}")]
    public ActionResult<Project> GetById(int project_id) {
        Project? project = _projectservice.GetById(project_id);

        if (project != null) {
            return project;
        } else {
            return NotFound();
        }
    }
    
    [HttpPut]
    public IActionResult Update([FromQuery]int project_id, Project updated_project) {
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery]int project_id) {
        return Ok();
    }

    [HttpPut]
    public IActionResult UserUpdate([FromQuery]int project_id, [FromQuery]int user_id, [FromQuery]int role_id, [FromQuery]bool remove) {
        return Ok();
    }
}