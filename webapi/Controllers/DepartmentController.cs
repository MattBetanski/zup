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
        return Ok();
    }

    [HttpGet]
    public ActionResult<Department> GetDepartmentById([FromQuery] long department_id) {
        return Ok();
    }

    // gets all the departments the user belongs to
    [HttpGet]
    [Route("all")]
    public ActionResult<List<Department>> GetUsersDepartments() {
        return Ok();
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
}
