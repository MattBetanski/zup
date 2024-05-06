using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapi.Data;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("item/comment")]
[Authorize]
public class ItemCommentController : ControllerBase {
    // copied from ItemController, if seperated services this should change to reflect that
    // private ItemService _itemservice;

    public ItemCommentController(/*ItemService service*/) {
        // _itemservice = service;
    }

    [HttpPost]
    public IActionResult LeaveComment([FromQuery] long item_id, [FromBody] ItemCommentBody comment_info) {
        return Ok();
    }

    [HttpGet]
    public ActionResult<List<ItemComment>> GetComments([FromQuery] long item_id) {
        return Ok();
    }

    [HttpPut]
    public IActionResult EditComment([FromQuery] long item_id) {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteComment([FromQuery] long item_id) {
        return Ok();
    }
}
