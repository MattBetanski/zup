
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
    DepartmentService _departmentservice;

    public ProjectController(ProjectService pservice, UserService uservice, DepartmentService dservice) {
        _projectservice = pservice;
        _userservice = uservice;
        _departmentservice = dservice;
    }

    /// <summary>
    /// Creates a new project
    /// </summary>
    /// <param name="project_info"></param>
    /// <returns></returns>
    /// <response code="204">Project created successfully</response>
    /// <response code="400">A project with the provided name already exists within the department</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not permitted to create projects in the departmentt</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateProject([FromBody] ProjectBody project_info) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Project new_project = new Project {
                Name = project_info.Name,
                Description = project_info.Description,
                DepartmentId = project_info.DepartmentId,
                CreatedDate = DateTime.UtcNow
            };

            if (self.UserId != _departmentservice.getOwner(project_info.DepartmentId).UserId)
                return StatusCode(403, "You are not permitted to create projects this department");
            else {
                new_project = _projectservice.Create(new_project);
                return NoContent();
            }
            // after roles created, come back and assign user maximum roles
        }
        catch (ObjectNameInUseException pniue) {
            return BadRequest(pniue.Message);
        }
        catch (AccessNotAllowedException anae) {
            return NotFound(anae.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Gets a project with its ID
    /// </summary>
    /// <param name="project_id"></param>
    /// <returns></returns>
    /// <response code="200">Project found</response>
    /// <response code="404">Project not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<Project> GetProjectById([FromQuery] long project_id) {
        try {
            Project project = _projectservice.GetById(project_id);
            return project;
        }
        catch (DataNotFoundException dnfe) {
            return NotFound(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPut]
    public IActionResult UpdateProject([FromQuery]long project_id, [FromBody] ProjectBody project_info) {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteProject([FromQuery]long project_id) {
        return Ok();
    }

    /// <summary>
    /// Updates a user's role in a project or adds them to the project if they are not already in it
    /// </summary>
    /// <param name="project_id"></param>
    /// <param name="user_id"></param>
    /// <param name="role_id"></param>
    /// <returns></returns>
    /// <response code="204">User added to project successfully</response>
    /// <response code="400">Selected user has already been added to the project</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not permitted to add users to projects in this department</response>
    /// <response code="500">Internal server error</response>
    [HttpPut]
    [Route("role")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateRole([FromQuery] long project_id, [FromQuery] long user_id, [FromQuery] long role_id) {
        try {
            IActionResult actionResult;
            if(_projectservice.checkIfInProject(user_id, project_id)) {
                actionResult = UserRoleUpdate(project_id, user_id, role_id);
            }
            else {
                actionResult = AddMemberToProject(project_id, user_id, role_id);
            }

            return actionResult;
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
    
    [NonAction]
    public IActionResult AddMemberToProject([FromQuery] long project_id, [FromQuery] long user_id, [FromQuery] long role_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Department department = _projectservice.GetDepartment(project_id);

            if (self.UserId != _departmentservice.getOwner(department.DepartmentId).UserId)
                return StatusCode(403, "You are not permitted to add users to projects in this department");

            ProjectUserRole projectUserRole = new ProjectUserRole {
                ProjectId = project_id,
                UserId = user_id,
                RoleId = role_id
            };

            _projectservice.AssignUserRole(projectUserRole);
            return NoContent();
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (UserAlreadyInGroupException uaige) {
            return BadRequest(uaige.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [NonAction]
    public IActionResult UserRoleUpdate([FromQuery]long project_id, [FromQuery]long user_id, [FromQuery]long role_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Department department = _projectservice.GetDepartment(project_id);
            
            if (self.UserId != _departmentservice.getOwner(department.DepartmentId).UserId)
                return StatusCode(403, "You are not permitted to update user roles in this department");
            else {
                _projectservice.UpdateUserRole(project_id, user_id, role_id);
                return Ok();
            }
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (DataNotFoundException dnfe) {
            return NotFound(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("member")]
    public IActionResult RemoveUserFromProject([FromQuery]long project_id, [FromQuery]long user_id) {
        return Ok();
    }

    /// <summary>
    /// Gets department information by project id
    /// </summary>
    /// <param name="project_id"></param>
    /// <returns>Department information</returns>
    /// <response code="200">Returns department information</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not a member of the department</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("department")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Department> GetDepartmentByProjectId([FromQuery] long project_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Department department = _departmentservice.getByProjectId(project_id);
            if (!_departmentservice.checkIfInDepartment(self.UserId, department.DepartmentId))
                return StatusCode(403, "You are not permitted to view this department");
            return Ok(department);
        }
        catch (AccessNotAllowedException anea) {
            return Unauthorized(anea.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}