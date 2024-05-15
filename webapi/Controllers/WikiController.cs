using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapi.Data;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("wiki")]
[Authorize]
public class WikiController : ControllerBase {
    private WikiService _wikiservice;
    private DepartmentService _deparmtnetserivce;
    private RoleService _roleservice;
    private UserService _userservice;

    public WikiController(WikiService wservice, DepartmentService dservice, RoleService rservice, UserService uservice) {
        _wikiservice = wservice;
        _deparmtnetserivce = dservice;
        _roleservice = rservice;
        _userservice = uservice;
    }

    /// <summary>
    /// Creates a new wiki
    /// </summary>
    /// <param name="department_id"></param>
    /// <param name="wiki_info"></param>
    /// <returns></returns>
    /// <response code="204">Wiki created successfully</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User does not have the proper level to create a wiki</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateWiki([FromQuery] long department_id, [FromBody] WikiBody wiki_info) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine($"bool: {self.UserId == _deparmtnetserivce.getOwner(department_id).UserId}");
            if (self.UserId == _deparmtnetserivce.getOwner(department_id).UserId || _roleservice.checkWikiLevel(self.UserId, RoleLevel.Modify)) {
                WikiPage new_wiki = new WikiPage() {
                    DepartmentId = department_id,
                    Title = wiki_info.Title,
                    Content = wiki_info.Content,
                    CreatedDate = DateTime.UtcNow
                };

                new_wiki = _wikiservice.Create(new_wiki);
                return NoContent();
            }
            else
                return StatusCode(403, "You are not permitted to create wikis in this department");
        }
        catch (AccessNotAllowedException ex) {
            return StatusCode(403, ex.Message);
        }
        catch (DataNotFoundException ex) {
            return StatusCode(404, ex.Message);
        }
        catch (ObjectNameInUseException ex) {
            return StatusCode(400, ex.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }    

    /// <summary>
    /// Returns all wikis in a department without the content
    /// </summary>
    /// <param name="department_id"></param>
    /// <returns></returns>
    /// <response code="200">Returns a list of wikis</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User does not have the proper level to view wikis</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(typeof(List<SimpleWiki>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<SimpleWiki>> GetDepartmentsWikis([FromQuery] long department_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (self.UserId == _deparmtnetserivce.getOwner(department_id).UserId || _roleservice.checkWikiLevel(self.UserId, RoleLevel.Read)) {
                List<WikiPage> wikis = _wikiservice.GetByDepartment(department_id);
                List<SimpleWiki> simple_wikis = new List<SimpleWiki>();

                foreach (WikiPage wiki in wikis) {
                    simple_wikis.Add(new SimpleWiki() {
                        WikiId = wiki.WikiPageId,
                        Title = wiki.Title,
                        CreatedDate = wiki.CreatedDate,
                        DepartmentId = wiki.DepartmentId
                    });
                }

                return simple_wikis;
            }
            else
                return StatusCode(403, "You are not permitted to view wikis in this department");
        }
        catch (AccessNotAllowedException anae) {
            return Unauthorized(anae.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Returns the content of a wiki
    /// </summary>
    /// <param name="wiki_id"></param>
    /// <returns></returns>
    /// <response code="200">Returns the wiki content</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User does not have the proper level to view the wiki</response>
    /// <response code="404">Wiki not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(WikiPage), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<WikiPage> GetWikiContents([FromQuery] long wiki_id) {
        // content will return as a string
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (_roleservice.checkWikiLevel(self.UserId, RoleLevel.Read)) {
                WikiPage wiki = _wikiservice.GetWiki(wiki_id);
                return wiki;
            }
            else
                return StatusCode(403, "You are not permitted to view this wiki");
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

    [HttpPut]
    public IActionResult UpdateWiki([FromQuery] long wiki_id, [FromBody] WikiBody wiki_info) {
        return Ok();
    }
}