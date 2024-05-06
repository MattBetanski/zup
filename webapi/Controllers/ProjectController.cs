using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using webapi.Data;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Authorize] // requires authorization for the entire controller, can also be put indiviually on each route
[ApiController]
[Route("project")]
public class ProjectController : ControllerBase {
    ProjectService _projectservice;
    
    public ProjectController(ProjectService service) {
        _projectservice = service;
    }

    // [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    // [HttpPost]
    // public IActionResult Create(Project new_project) {
    //     return Ok();
    // }

    // [HttpGet("{project_id}")]
    // public ActionResult<Project> GetById(int project_id) {
    //     Project? project = _projectservice.GetById(project_id);

    //     if (project != null) {
    //         return project;
    //     } else {
    //         return NotFound();
    //     }
    // }

    [HttpPost]
    public IActionResult CreateProject([FromBody] ProjectBody project_info) {
        return Ok();
    }

    [HttpGet]
    public ActionResult<Project> GetProjectById([FromQuery] long project_id) {
        return Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateProject([FromQuery]long project_id, [FromBody] ProjectBody project_info) {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteProject([FromQuery]long project_id) {
        return Ok();
    }

    [HttpPut]
    [Route("roles")]
    public IActionResult UserRoleUpdate([FromQuery]long project_id, [FromQuery]long user_id, [FromQuery]long role_id, [FromQuery]bool remove = false) {
        return Ok();
    }
}