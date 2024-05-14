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

            IActionResult create() {
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

            if(self.UserId == _departmentservice.getOwner(department_id).UserId)
                return create();
            else if(!_departmentservice.checkIfInDepartment(self.UserId, department_id))
                return StatusCode(403, "Cannot create an item in this department");
            else if (!_projectservice.checkIfInProject(self.UserId, item_info.ProjectId)) 
                return StatusCode(403, "Cannot create an item in this project");
            else if (!_roleservice.checkItemLevel(self.UserId, RoleLevel.Modify))
                return StatusCode(403, "You do not have permission to create an item in this project");
            else {
                return create();
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

    /// <summary>
    /// Gets an item by its ID
    /// </summary>
    /// <param name="item_id"></param>
    /// <returns></returns>
    /// <response code="200">Item found</response>
    /// <response code="400">Invalid data</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<Item> GetItemById([FromQuery] long item_id) {
        try {
            Item item = _itemservice.GetById(item_id);
            return item;
        }
        catch (DataNotFoundException dnfe) {
            return BadRequest(dnfe.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Gets children of an item
    /// </summary>
    /// <param name="parent_id"></param>
    /// <returns></returns>
    /// <response code="200">Returns list of children items</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("children")]
    [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<Item>> GetChildItems([FromQuery] long parent_id) {
        try {
            List<Item> children = _itemservice.GetChildren(parent_id);
            return children;
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Gets items with a filter
    /// </summary>
    /// <param name="project_id"></param>
    /// <param name="type">Optional</param>
    /// <param name="state">Optional</param>
    /// <returns></returns>
    /// <response code="200">Returns list of items based on filter query</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User does not have the proper level to view items in this project</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [Route("filter")]
    [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<Item>> GetItemByFilter([FromQuery] long project_id, [FromQuery] Models.Type? type, [FromQuery] State? state) {
        try {
            long department_id = _projectservice.GetDepartment(project_id).DepartmentId;
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(!checkIfPermitted(false, RoleLevel.Read, department_id, project_id, self.UserId))
                return StatusCode(403, "You do not have permission to view items in this project");

            return _itemservice.GetWithFilter(project_id, type, state);
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
    /// Updates an item
    /// </summary>
    /// <param name="item_id"></param>
    /// <param name="item_info"></param>
    /// <returns></returns>
    /// <response code="204">Item updated successfully</response>
    /// <response code="400">Invalid data</response>
    /// <response code="401">A problem occured validating the user's token</response>
    /// <response code="403">User does not have the proper level to modify this item</response>
    /// <response code="500">Internal server error</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateItem([FromQuery] long item_id, [FromBody] ItemBodyU item_info) {
        try {
            Item item = _itemservice.GetById(item_id);

            if(item == null)
                return BadRequest("Item not found");

            long department_id = _projectservice.GetDepartment(item.ProjectId).DepartmentId;
            User self = _userservice.getSelf(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(!checkIfPermitted(true, RoleLevel.Modify, department_id, item.ProjectId, self.UserId))
                return StatusCode(403, "You do not have permission to modify this item");

            
            item.Name = item_info.Name;
            item.Description = item_info.Description;
            item.State = item_info.State;
            item.ParentId = item_info.ParentId;

            _itemservice.Update(item);
            return NoContent();
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

    [HttpDelete]
    public IActionResult DeleteItem([FromQuery] long item_id) {
        return Ok();
    }

    [NonAction]
    public bool checkIfPermitted(bool needsOwner, RoleLevel minimumLevel, long department_id, long project_id, long user_id) {
        bool permitted = false;

        if(needsOwner) {
            if(user_id == _departmentservice.getOwner(department_id).UserId)
                permitted = true;
        }
        else {
            if(_departmentservice.checkIfInDepartment(user_id, department_id) && _projectservice.checkIfInProject(user_id, project_id) && _roleservice.checkItemLevel(user_id, minimumLevel))
                permitted = true;
        }

        if(user_id == _departmentservice.getOwner(department_id).UserId)
            permitted = true;

        return permitted;
    }
}
