using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapi.Data;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("role")]
[Authorize]
public class RoleController : ControllerBase {
    private RoleService _roleservice;
    private UserService _userservice;
    private DepartmentService _departmentservice;

    public RoleController(RoleService rservice, UserService uservice, DepartmentService dservice) {
        _roleservice = rservice;
        _userservice = uservice;
        _departmentservice = dservice;
    }

    /// <summary>
    /// Creates a new role
    /// </summary>
    /// <param name="role_info"></param>
    /// <returns></returns>
    /// <response code="204">Role created successfully</response>
    /// <response code="400">A role with the provided name already exists within the department</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not permitted to create roles in the department</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateRole([FromBody] RoleBody role_info) {
        try {
            Role new_role = new Role() {
                Name = role_info.Name,
                Description = role_info.Description,
                DepartmentId = role_info.DepartmentId,
                ItemLevel = role_info.ItemLevel,
                WikiLevel = role_info.WikiLevel,
                WikiDelete = role_info.WikiDelete
            };

            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (self.UserId != _departmentservice.getOwner(role_info.DepartmentId).UserId)
                return Forbid("You are not permitted to create roles in this department");
            else {
                new_role = _roleservice.Create(new_role);
                return NoContent();
            }
        }
        catch (AccessNotAllowedException) {
            return Unauthorized();
        }
        catch (ObjectNameInUseException oniue) {
            return BadRequest(oniue.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Gets a role by its ID
    /// </summary>
    /// <param name="role_id"></param>
    /// <returns></returns>
    /// <response code="200">Returns the role</response>
    /// <response code="400">Invalid data</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(Role), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<Role> GetRoleById([FromQuery] long role_id) {
        try {
            Role role = _roleservice.GetById(role_id);
            return role;
        }
        catch (DataNotFoundException dnfe) {
            return BadRequest(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Deletes a role by its ID
    /// </summary>
    /// <param name="role_id"></param>
    /// <returns></returns>
    /// <response code="204">Role deleted successfully</response>
    /// <response code="400">Invalid data</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteRole([FromQuery] long role_id) {
        try {
            _roleservice.Delete(role_id);
            return NoContent();
        }
        catch (DataNotFoundException dnfe) {
            return BadRequest(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Gets the user's item level for a project
    /// </summary>
    /// <param name="project_id"></param>
    /// <returns></returns>
    /// <response code="200">Returns the user's item level</response>
    /// <response code="400">Invalid data</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("level/item")]
    [ProducesResponseType(typeof(RoleLevel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<RoleLevel> GetUsersItemLevel([FromQuery] long project_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Role? role = _roleservice.GetByUserId(self.UserId, project_id);
            
            if (role == null)
                return RoleLevel.NoAccess;
            else
                return role.ItemLevel;
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (DataNotFoundException dnfe) {
            return BadRequest(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Gets the user's wiki level for a project
    /// </summary>
    /// <param name="project_id"></param>
    /// <returns></returns>
    /// <response code="200">Returns the user's wiki level</response>
    /// <response code="400">Invalid data</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("level/wiki")]
    public ActionResult<RoleLevel> GetUsersWikiLevel([FromQuery] long project_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Role? role = _roleservice.GetByUserId(self.UserId, project_id);
            
            if (role == null)
                return RoleLevel.NoAccess;
            else
                return role.WikiLevel;
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (DataNotFoundException dnfe) {
            return BadRequest(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}