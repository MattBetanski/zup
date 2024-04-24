using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase {
    ProjectService _projectservice;
    
    public ProjectController(ProjectService service) {
        _projectservice = service;
    }

    [HttpGet]
    public IEnumerable<Project> GetAll() {
        return _projectservice.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Project> GetById(int id) {
        Project? project = _projectservice.GetById(id);

        if (project != null) {
            return project;
        } else {
            return NotFound();
        }
    }
}