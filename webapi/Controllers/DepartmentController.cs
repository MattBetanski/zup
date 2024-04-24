using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase {
    private DepartmentService _departmentservice;

    public DepartmentController(DepartmentService service) {
        _departmentservice = service;
    }

    [HttpGet]
    public IEnumerable<Department> GetAll() {
        return _departmentservice.GetAll();
    }
}
