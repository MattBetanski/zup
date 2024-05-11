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

    // [HttpPut]
    // public IActionResult AssignRole([FromQuery] long role_id, [FromQuery] long user_id) {
    //     try {

    //     }
    // }
}