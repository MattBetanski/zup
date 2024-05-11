using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapi.Data;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("item")]
[Authorize]
public class ItemController : ControllerBase {
    private ItemService _itemservice;
    private DepartmentService _departmentservice;
    private ProjectService _projectservice;
    private RoleService _roleservice;
    private UserService _userservice;

    public ItemController(ItemService iservice, DepartmentService dservice, ProjectService pservice, RoleService rservice, UserService uservice) {
        _itemservice = iservice;
        _departmentservice = dservice;
        _projectservice = pservice;
        _roleservice = rservice;
        _userservice = uservice;
    }

    /// <summary>
    /// Creates an item
    /// </summary>
    /// <param name="item_info"></param>
    /// <returns></returns>
    /// <response code="204">Item created successfully</response>
    /// <response code="400">Invalid data</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User does not have the proper level to create an item</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateItem([FromBody] ItemBody item_info) {
        try {
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));
            long department_id = _projectservice.GetDepartment(item_info.ProjectId).DepartmentId;


            if(!_departmentservice.checkIfInDepartment(self.UserId, department_id))
                return StatusCode(403, "Cannot create an item in this department");
            else if (!_projectservice.checkIfInProject(self.UserId, item_info.ProjectId)) 
                return StatusCode(403, "Cannot create an item in this project");
            else if (!_roleservice.checkItemLevel(self.UserId, RoleLevel.Modify))
                return StatusCode(403, "You do not have permission to create an item in this project");
            else {
                Item item = new Item {
                    OwnerId = self.UserId,
                    Name = item_info.Name,
                    Description = item_info.Description,
                    Type = item_info.Type,
                    State = item_info.State,
                    ProjectId = item_info.ProjectId,
                    ParentId = item_info.ParentId,
                    CreatedDate = DateTime.UtcNow
                };

                _itemservice.Create(item);
                return NoContent();
            }
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

    [HttpGet]
    public ActionResult<Item> GetItemById([FromQuery] long item_id) {
        return Ok();
    }

    [HttpGet]
    [Route("filter")]
    public ActionResult<List<Item>> GetItemByFilter([FromQuery] long project_id, [FromQuery] Models.Type? type, [FromQuery] State? state) {
        // confirm type and state default to null
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateItem([FromQuery] long item_id, [FromBody] ItemBodyU item_info) {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteItem([FromQuery] long item_id) {
        return Ok();
    }

    [HttpPut]
    [Route("assign")]
    public IActionResult AssignItem([FromQuery] long item_id, [FromQuery] long user_id) {
        return Ok();
    }

    [HttpDelete]
    [Route("assign")]
    public IActionResult UnassignItem([FromQuery] long item_id, [FromQuery] long user_id) {
        return Ok();
    }
}
