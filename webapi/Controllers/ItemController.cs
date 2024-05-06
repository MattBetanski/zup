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
    // private ItemService _itemservice;

    public ItemController(/*ItemService service*/) {
        // _itemservice = service;
    }

    [HttpPost]
    public IActionResult CreateItem([FromBody] ItemBody item_info) {
        // set owner to be whoever is creating the item
        return Ok();
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
