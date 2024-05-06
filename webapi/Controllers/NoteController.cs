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
    // private NoteService _noteservice;

    public NoteController(/*NoteService service*/) {
        // _noteservice = service;
    }

    [HttpPut]
    [Route("rate")]
    public IActionResult RateNote([FromQuery] long note_id, [FromQuery] bool? rating = null) {
        // if rating is not set, remove rating (this should be a trigger)
        // ask copilot how to run raw sql file within entity framework core
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteNote([FromQuery] long note_id) {
        return Ok();
    }
}