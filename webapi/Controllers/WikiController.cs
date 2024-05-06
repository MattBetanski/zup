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

    [HttpGet]
    [Route("all")]
    public ActionResult<List<WikiPage>> GetDepartmentsWikis([FromQuery] long department_id) {
        return Ok();
    }

    [HttpGet]
    public ActionResult<WikiPage> GetWikiContents([FromQuery] long wiki_id) {
        return Ok();
    }
}