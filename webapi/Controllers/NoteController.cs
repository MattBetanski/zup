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
    private DepartmentService _departmentservice;
    private ProjectService _projectservice;
    private UserService _userservice;

    public NoteController(NoteService service, DepartmentService dservice, ProjectService pservice, UserService uservice) {
        _noteservice = service;
        _departmentservice = dservice;
        _projectservice = pservice;
        _userservice = uservice;
    }

    /// <summary>
    /// Get all notes from a department
    /// </summary>
    /// <param name="noteBody"></param>
    /// <returns></returns>
    /// <response code="200">Note created</response>
    /// <response code="401">A problem occured validating the token</response>
    /// <response code="403">You are not a member of the project</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Note> CreateNote([FromBody] NoteBody noteBody) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Note new_note = new Note() {
                Title = noteBody.Title,
                Content = noteBody.Content,
                DepartmentId = noteBody.DepartmentId,
                CreatedDate = DateTime.UtcNow,
                OwnerId = self.UserId,
            };

            if (self.UserId != _departmentservice.getOwner(new_note.DepartmentId).UserId || !_projectservice.checkIfInProject(new_note.ProjectId, self.UserId)) {
                throw new AccessNotAllowedException("You are not a member of the project");
            }

            new_note.OwnerId = self.UserId;
            _noteservice.Create(new_note);
            return new_note;
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Get all notes from a department
    /// </summary>
    /// <param name="note_id"></param>
    /// <returns></returns>
    /// <response code="200">Note found</response>
    /// <response code="401">A problem occured validating the token</response>
    /// <response code="404">Note not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    public ActionResult<Note> GetNoteById([FromQuery] long note_id) {
        try {
            Note note = _noteservice.GetNote(note_id);
            return note;
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);      
        }
        catch (DataNotFoundException dnfe) {
            return NotFound(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Rate a note
    /// </summary>
    /// <param name="note_id"></param>
    /// <param name="rating"></param>
    /// <returns></returns>
    /// <response code="204">Rating updated</response>
    /// <response code="401">A problem occured validating the token</response>
    /// <response code="404">Note not found</response>
    /// <response code="500">Internal server error</response>
    [HttpPut]
    [Route("rate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult RateNote([FromQuery] long note_id, [FromQuery] bool? rating = null) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (_noteservice.hasRated(note_id, self.UserId)) {
                if (rating == null) {
                    // remove rating
                    _noteservice.DeleteRating(note_id, self.UserId);
                }
                else {
                    // update rating
                    _noteservice.UpdateRating(note_id, self.UserId, (bool)rating);
                }
            }
            else {
                if (rating != null) {
                    NoteRating nr = new NoteRating() {
                        NoteId = note_id,
                        UserId = self.UserId,
                        Rate = (bool)rating
                    };
                    _noteservice.AddRating(nr);
                }
            }
            
            return NoContent();
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (DataNotFoundException dnfe) {
            return NotFound(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    public IActionResult DeleteNote([FromQuery] long note_id) {
        return Ok();
    }
}