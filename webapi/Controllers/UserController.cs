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
            }; // Initialize the User object.

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
