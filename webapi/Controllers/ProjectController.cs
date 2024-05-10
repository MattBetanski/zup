
using System.Security.Claims;
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
    UserService _userservice;
    
    public ProjectController(ProjectService pservice, UserService uservice) {
        _projectservice = pservice;
        _userservice = uservice;
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
        try {
            Console.WriteLine("here");
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Project new_project = new Project {
                Name = project_info.Name,
                Description = project_info.Description,
                DepartmentId = project_info.DepartmentId,
                CreatedDate = DateTime.UtcNow
            };

            new_project = _projectservice.Create(new_project);
            // after roles created, come back and assign user maximum roles
            return NoContent();
        }
        catch (ProjectNameInUseException pniue) {
            return BadRequest(pniue.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
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