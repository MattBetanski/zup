using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapi.Data;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;


[ApiController]
[Route("department")]
[Authorize]
public class DepartmentController : ControllerBase {
    private DepartmentService _departmentservice;
    private RoleService _roleservice;
    private UserService _userservice;

    public DepartmentController(DepartmentService dservice, UserService uservice, RoleService rservice) {
        _departmentservice = dservice;
        _roleservice =rservice;
        _userservice = uservice;
    }

    /// <summary>
    /// Creates a new department
    /// </summary>
    /// <param name="department_info"></param>
    /// <response code="204">Department was created successfully</response>
    /// <response code="400">Some data was not found, check exception message for details</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateDepartment([FromBody] DepartmentBody department_info) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Department new_dep = new Department {
                Name = department_info.Name,
                Description = department_info.Description,
                CreationDate = DateTime.UtcNow,
                Visibility = department_info.Visibility,
                OwnerId = self.UserId
            };

            Department post_creation_department = _departmentservice.create(new_dep);
            _departmentservice.AddUserToDepartment(self.UserId, post_creation_department.DepartmentId);

            return NoContent();
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (DataNotFoundException dnfe) {
            return BadRequest(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Gets a department by its id
    /// </summary>
    /// <param name="department_id"></param>
    /// <returns>The department with a matching ID</returns>
    /// <response code="200">Returns the department with the matching ID</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not a member of the department</response>
    /// <response code="404">Some data was not found, check exception message for details</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Department> GetDepartmentById([FromQuery] long department_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_departmentservice.checkIfInDepartment(self.UserId, department_id))
                return StatusCode(403, "Not a member of the department");

            Department department = _departmentservice.getById(department_id);
            return department;
        } 
        catch (DataNotFoundException dnfe) {
            return NotFound(dnfe.Message);
        } 
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Gets all departments that the user is a member of
    /// </summary>
    /// <returns>List of departments the user is a member of</returns>
    /// <response code="200">Returns a list of all departments the user is a member of</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<Department>> GetUsersDepartments() {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<Department> departments = _departmentservice.getByUser(self.UserId);
            return departments;
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Returns a list of all projects in the department
    /// </summary>
    /// <param name="department_id"></param>
    /// <returns>List of all projects in the department</returns>
    /// <response code="200">Returns a list of all projects in the department</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not a member of the department</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("projects")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<Project>> GetDepartmentsProjects([FromQuery] long department_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (!_departmentservice.checkIfInDepartment(self.UserId, department_id))
                return StatusCode(403, "Not a member of the department");
            
            List<Project> projects = _departmentservice.GetProjects(department_id);
            return projects;
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    public IActionResult UpdateDepartment([FromQuery] long department_id, [FromBody] DepartmentBody department_info) {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteDepartment([FromQuery] long department_id, [FromBody] DepartmentBody department_info) {
        return Ok();
    }

    /// <summary>
    /// Returns a list of all roles in the department
    /// </summary>
    /// <param name="department_id"></param>
    /// <returns>List of all roles in the department</returns>
    /// <response code="200">Returns a list of all roles in the department</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not a member of the department</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<Role>> GetDepartmentRoles([FromQuery] long department_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (!_departmentservice.checkIfInDepartment(self.UserId, department_id))
                return StatusCode(403, "You are not permitted view roles in this department");
            else {
                List<Role> roles = _departmentservice.GetRoles(department_id);
                return roles;
            }
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
    /// Returns a list of all members in the department
    /// </summary>
    /// <param name="project_id"></param>
    /// <returns>List of all members in the department</returns>
    /// <response code="200">Returns a list of all members in the department</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not a member of the department</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<UserAndRoleResponse> GetDepartmentMembersByProjectId([FromQuery] long project_id) {
        try {
            List<UserAndRoleResponse> user_list = new List<UserAndRoleResponse>();

            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            long department_id = _departmentservice.getByProjectId(project_id).DepartmentId;

            if (!_departmentservice.checkIfInDepartment(self.UserId, department_id))
                return StatusCode(403, "You are not permitted to view this department");
            else {
                List<User> members = _departmentservice.GetMembers(department_id);
                foreach(User mbr in members) {
                    UserAndRoleResponse UserAndRoleResponse = new UserAndRoleResponse {
                        Email = mbr.Email,
                        FirstName = mbr.FirstName,
                        LastName = mbr.LastName,
                        UserId = mbr.UserId
                    };
                    Role? role = _roleservice.GetByUserId(mbr.UserId, project_id);
                    Console.WriteLine($"role: {role}");
                    Console.WriteLine($"uid: {mbr.UserId}");
                    if (role != null) {
                        UserAndRoleResponse.RoleId = role.RoleId;
                        UserAndRoleResponse.RoleName = role.Name;
                    }
                    user_list.Add(UserAndRoleResponse);
                }

                return Ok(user_list);
            }
        }
        catch (AccessNotAllowedException anea) {
            return Unauthorized(anea.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }




    /// <summary>
    /// Invites a user to the department
    /// </summary>
    /// <param name="department_id"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    /// <response code="204">User was invited to the department</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not the owner of the department</response>
    /// <response code="404">Some data was not found, check exception message for details</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [Route("invite")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult InviteUserToDepartment([FromQuery] long department_id, [FromQuery] string email) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (self.UserId != _departmentservice.getOwner(department_id).UserId)
                return StatusCode(403, "You are not permitted to invite users to this department");
            else {
                _departmentservice.inviteUser(department_id, email);
                return NoContent();
            }
        }
        catch (AccessNotAllowedException anea) {
            return Unauthorized(anea.Message);
        }
        catch (DataNotFoundException dnfe) {
            return NotFound(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Revoke an invite to the department
    /// </summary>
    /// <param name="department_id"></param>
    /// <param name="invitee_id"></param>
    /// <returns></returns>
    /// <response code="204">Invite was revoked</response>
    /// <response code="400">Some data was not found, check exception message for details</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User is not the owner of the department</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete]
    [Route("invite")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult RevokeInviteToDepartment([FromQuery] long department_id, [FromQuery] long invitee_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(self.UserId != _departmentservice.getOwner(department_id).UserId)
                return StatusCode(403, "You are not permitted to revoke invites in this department");
            else {
                _departmentservice.RevokeInvite(department_id, invitee_id);
                return NoContent();
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

    /// <summary>
    /// Lists all the pending invites for the department
    /// </summary>
    /// <param name="department_id">Id of the department</param>
    /// <returns>List of all pending invites for the department</returns>
    /// <response code="200">Returns a list of all pending invites for the department.</response>
    /// <response code="400">Some data was not found, check exception message for details</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("invitations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<InvitesResponse>> GetDepartmentsPendingInvites([FromQuery] long department_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (self.UserId != _departmentservice.getOwner(department_id).UserId) 
                return StatusCode(403, "You are not permitted to view pending invites for this department");
            
            List<InvitesResponse> invitesList = _departmentservice.GetPendingInvites(department_id);
            return invitesList;
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (DataNotFoundException dnfe) {
            return BadRequest(dnfe.Message);
        }
    }

    // if have spare time:
    // make it pageinated
    // take in offset and count
    [HttpGet]
    [Route("note")]
    public ActionResult<List<Note>> GetDepartmentNotes([FromQuery] long department_id) {
        return Ok();
    }

    /// <summary>
    /// Checks if the user is the owner of the department
    /// </summary>
    /// <param name="department_id"></param>
    /// <param name="user_id"></param>
    /// <returns></returns>
    /// <response code="200">Returns true if the user is the owner of the department, false otherwise</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="404">Some data was not found, check exception message for details</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("owner")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<bool> checkIfDepartmentOwner([FromQuery] long department_id, [FromQuery] long user_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User owner = _departmentservice.getOwner(department_id);

            if (self.UserId == owner.UserId)
                return true;
            else
                return false;
        }
        catch (DataNotFoundException dnfe) {
            return NotFound(dnfe.Message);
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }

    }


    // FOR TESTING ONLY //
    [HttpPut]
    [Route("testingadd")]
    public IActionResult AddUserToDepartment_DEBUG([FromQuery] long user_id, [FromQuery] long department_id) {
        try {
            _departmentservice.AddUserToDepartment(user_id, department_id);
            return Ok();
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }
}
