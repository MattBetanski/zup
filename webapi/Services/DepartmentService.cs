using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class DepartmentService {
    private readonly ZupContext _context;

    public DepartmentService(ZupContext context) {
        _context = context;
    }

    public Department create(Department department) {
        try {
            _context.Department.Add(department);
            _context.SaveChanges();

            return department;
        } catch {
            throw;
        }
    }
}
