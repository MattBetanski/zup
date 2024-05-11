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

    [HttpPost]
    public IActionResult CreateItem([FromBody] ItemBody item_info) {
        // set owner to be whoever is creating the item
        // check if in department (get from porject id)x
        // check if in project x
        // check if level is high enough
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
