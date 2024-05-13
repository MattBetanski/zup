using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapi.Data;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase {
    UserService _userservice;
    AuthService _authservice;
    DepartmentService _departmentservice;
    public UserController(UserService service, AuthService aservice, DepartmentService dservice) {
        _userservice = service;
        _authservice = aservice;
        _departmentservice = dservice;
    }

    /// <summary>
    /// Creates a new user account
    /// </summary>
    /// <param name="account_info"></param>
    /// <returns>JSON web token for user authetication</returns>
    /// <response code="200">User account created successfully, returns JSON web token</response>
    /// <response code="400">A user with the provided username or email already exists</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateAccount([FromBody] UserBody account_info) {
        try {
            User created_user = new User {
                Username = account_info.Username,
                HashedPassword = _userservice.HashPassword(account_info.Password),
                Email = account_info.Email,
                FirstName = account_info.FirstName,
                LastName = account_info.LastName,
                JoinDate = DateTime.UtcNow
            };

            _userservice.create(created_user);

            string token = _authservice.GenerateJwtToken(created_user);
            return Ok(new { Token = token });
        } 
        catch (CredentialsInUseException ciue) {
            return BadRequest(ciue.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Logs in a user
    /// </summary>
    /// <param name="creds"></param>
    /// <returns>JSON web token for user authentication</returns>
    /// <response code="200">User logged in successfully, returns JSON web token</response>
    /// <response code="400">Invalid credentials</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Login([FromBody] LoginBody creds) {
        try {
            User logged_user = _userservice.login(creds.Username, creds.Password);
            string token = _authservice.GenerateJwtToken(logged_user);
            return Ok(new { Token = token });
        }
        catch (InvalidCredentialsException ice) {
            return BadRequest(ice.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Gets list of department names and ids that the user has pending invites to
    /// </summary>
    /// <returns></returns>
    /// <response code="200">List of department names and ids</response>
    /// <response code="400">Issue getting invite name</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("invitations")]
    [Authorize]
    [ProducesResponseType(typeof(List<MinimalDepartment>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<MinimalDepartment>> GetUsersInvites() {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<MinimalDepartment> invites = _userservice.GetInvites(self.UserId);
            return invites;
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
    /// Updates the status of a department invite
    /// </summary>
    /// <param name="department_id"></param>
    /// <param name="response"></param>
    /// <returns></returns>
    /// <response code="204">Invite response updated</response>
    /// <response code="400">Invite is no longer valid or user is already in department</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="404">No invite found for user and department</response>
    /// <response code="500">Internal server error</response>
    [HttpPut]
    [Route("invitations")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult RespondToDepartmentInvite([FromQuery] long department_id, [FromQuery] InviteResponse response) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (_userservice.checkIfResponded(self.UserId, department_id))
                throw new InviteExpiredException("The invite is no longer valid");
            if (_departmentservice.checkIfInDepartment(self.UserId, department_id))
                throw new UserAlreadyInGroupException("User is already part of selected department");

            DepartmentInvite di = _userservice.RespondToInvite(self.UserId, department_id, response);
            if (response == InviteResponse.Accepted)
                _departmentservice.AddUserToDepartment(self.UserId, department_id);
            return NoContent();
        }
        catch (InviteExpiredException iee) {
            return BadRequest(iee.Message);
        }
        catch (UserAlreadyInGroupException uaige) {
            return BadRequest(uaige.Message);
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
}
