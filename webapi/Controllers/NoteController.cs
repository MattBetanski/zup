using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapi.Data;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("note")]
[Authorize]
public class NoteController : ControllerBase {
    private NoteService _noteservice;
    private UserService _userservice;

    public NoteController(NoteService service, UserService uservice) {
        _noteservice = service;
        _userservice = uservice;
    }

    [HttpPut]
    [Route("rate")]
    public IActionResult RateNote([FromQuery] long note_id, [FromQuery] bool? rating = null) {
        // if rating is not set, remove rating (this should be a trigger)
        // ask copilot how to run raw sql file within entity framework core
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (rating == null) {
                // remove rating
                //_noteservice.RemoveRating(note_id);
            }
            else {
                // add rating
            
            }
            return NoContent();
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    public IActionResult DeleteNote([FromQuery] long note_id) {
        return Ok();
    }
}