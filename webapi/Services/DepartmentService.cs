using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class DepartmentService {
    private readonly ZupContext _context;

    public DepartmentService(ZupContext context) {
        _context = context;
    }

    public IEnumerable<Department> GetAll() {
        var departments = (from dept in _context.Department 
                            select dept).AsNoTracking().ToList();

        return departments;
    }
}
