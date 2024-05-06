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
    // private WikiService _wikiservice;

    public WikiController(/*WikiService service*/) {
        // _wikiservice = service;
    }

    [HttpPost]
    public IActionResult CreateWiki([FromQuery] long department_id, [FromBody] WikiBody wiki_info) {
        return Ok();
    }    

    [HttpGet]
    [Route("all")]
    public ActionResult<List<WikiPage>> GetDepartmentsWikis([FromQuery] long department_id) {
        // don't return the content here
        return Ok();
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