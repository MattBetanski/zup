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
        catch (DataNotFoundException dnfe) {
            return BadRequest(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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
            return BadRequest(dnfe.Message);
        } 
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    // gets all the departments the user belongs to
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

    [HttpGet]
    [Route("projects")]
    public ActionResult<List<Project>> GetDepartmentsProjects([FromQuery] long department_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (!_departmentservice.checkIfInDepartment(self.UserId, department_id))
                return StatusCode(403, "Not a member of the department");
            
            List<Project> projects = _departmentservice.GetProjects(department_id);
            return projects;
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

    [HttpGet]
    [Route("roles")]
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
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("members")]
    public ActionResult<UserAndRoleBody> GetDepartmentMembersByProjectId([FromQuery] long project_id) {
        try {
            List<UserAndRoleBody> user_list = new List<UserAndRoleBody>();

            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            long department_id = _departmentservice.getByProjectId(project_id).DepartmentId;

            if (!_departmentservice.checkIfInDepartment(self.UserId, department_id))
                return StatusCode(403, "You are not permitted to view this department");
            else {
                List<User> members = _departmentservice.GetMembers(department_id);
                foreach(User mbr in members) {
                    UserAndRoleBody userAndRoleBody = new UserAndRoleBody {
                        Email = mbr.Email,
                        FirstName = mbr.FirstName,
                        LastName = mbr.LastName
                    };

                    Role? role = _roleservice.GetByUserId(mbr.UserId);
                    if (role != null) {
                        userAndRoleBody.RoleId = role.RoleId;
                        userAndRoleBody.RoleName = role.Name;
                    }

                    user_list.Add(userAndRoleBody);
                }

                return Ok(user_list);
            }
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("invite")]
    public IActionResult InviteUserToDepartment([FromQuery] long department_id, [FromQuery] string email) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (self.UserId != _departmentservice.getOwner(department_id).UserId)
                return StatusCode(403, "You are not permitted to invite users to this department");
            else {
                _departmentservice.inviteUser(department_id, email);
                return Ok();
            }
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
    [Route("invite")]
    public IActionResult RevokeInviteToDepartment([FromQuery] long department_id, [FromQuery] string email) {
        return Ok();
    }

    [HttpGet]
    [Route("invitations")]
    public ActionResult<List<DepartmentInvite>> GetPendingInvites([FromQuery] long department_id) {
        return Ok();
    }

    [HttpPut]
    [Route("invitations")]
    public IActionResult RespondToInvite([FromQuery] long department_id, [FromQuery] bool response) {
        return Ok();
    }

    // if have spare time:
    // make it pageinated
    // take in offset and count
    [HttpGet]
    [Route("note")]
    public ActionResult<List<Note>> GetDepartmentNotes([FromQuery] long department_id) {
        return Ok();
    }

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
