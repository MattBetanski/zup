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

    public DepartmentController(DepartmentService service) {
        _departmentservice = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateDepartment([FromBody] DepartmentBody department_info) {
        try {
            

            Department new_dep = new Department {
                Name = department_info.Name,
                Description = department_info.Description,
                CreationDate = DateTime.UtcNow,
                Visibility = department_info.Visibility
            };

            _departmentservice.create(new_dep);
            return NoContent();
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
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<Department>> GetUsersDepartments() {
        try {
            string? id_string = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id_string == null)
                throw new AccessNotAllowedException("There was an issue authenticating the claimed user's ID");

            if (!long.TryParse(id_string, out long user_id))
                throw new AccessNotAllowedException("There was an issue parsing the authenticated user's ID");

            List<Department> departments = _departmentservice.getByUser(user_id);
            return departments;
        }
        catch (DataNotFoundException dnfe) {
            return NotFound(dnfe.Message);
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
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateDepartment([FromQuery] long department_id, [FromBody] DepartmentBody department_info) {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteDepartment([FromQuery] long department_id, [FromBody] DepartmentBody department_info) {
        return Ok();
    }

    [HttpPost]
    [Route("invite")]
    public IActionResult InviteUserToDepartment([FromQuery] long department_id, [FromQuery] long invitee_id) {
        try {
            string? id_string = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (id_string == null)
                throw new AccessNotAllowedException("There was an issue authenticating the claimed user's ID");

            if (!long.TryParse(id_string, out long user_id))
                throw new AccessNotAllowedException("There was an issue parsing the authenticated user's ID");
            
            if (user_id != _departmentservice.getOwner(department_id).UserId)
                return Forbid("You are not permitted to invite users to this department");
            else {
                _departmentservice.inviteUser(department_id, invitee_id);
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
    public IActionResult RevokeInviteToDepartment([FromQuery] long department_id, [FromQuery] long invitee_id) {
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