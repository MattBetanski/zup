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
        return Ok();
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
}
