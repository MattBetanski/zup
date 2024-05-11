using System.Runtime.InteropServices.JavaScript;
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
    public UserController(UserService service, AuthService aservice) {
        _userservice = service;
        _authservice = aservice;
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

    [HttpGet]
    [Route("invitations")]
    public ActionResult<List<Invite>> GetInvites() {
        return Ok();
    }
}
