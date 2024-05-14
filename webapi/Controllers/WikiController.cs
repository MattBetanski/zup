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
    private RoleService _roleservice;
    private UserService _userservice;

    public WikiController(WikiService wservice, RoleService rservice, UserService uservice) {
        _wikiservice = wservice;
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

            if (_roleservice.checkWikiLevel(self.UserId, RoleLevel.Modify)) {
                WikiPage new_wiki = new WikiPage() {
                    DepartmentId = department_id,
                    Title = wiki_info.Title,
                    Content = wiki_info.Content
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

    [HttpGet]
    [Route("all")]
    public ActionResult<List<SimpleWiki>> GetDepartmentsWikis([FromQuery] long department_id) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (_roleservice.checkWikiLevel(self.UserId, RoleLevel.Read)) {
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

    [HttpGet]
    public ActionResult<WikiPage> GetWikiContents([FromQuery] long wiki_id) {
        // content will return as a string
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateWiki([FromQuery] long wiki_id, [FromBody] WikiBody wiki_info) {
        return Ok();
    }
}