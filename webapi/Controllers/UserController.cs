using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase {
    UserService _userservice;
    AuthService _authservice;
    public UserController(UserService service, AuthService aservice) {
        _userservice = service;
        _authservice = aservice;
    }

    [HttpPost]
    [Route("create")]
    public IActionResult CreateAccount(User new_user) {
        try {
            User created_user = _userservice.create(new_user);
            string token = _authservice.GenerateJwtToken(new_user);
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
    public IActionResult Login([FromBody] LoginModel creds) {
        try {
            string username = creds.Username;
            Console.Write($"here: {username}");
            User logged_user = _userservice.login(username);
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
}
