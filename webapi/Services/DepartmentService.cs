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

    public Department getById(long department_id) {
        try {
            Department? department = (from dep in _context.Department
                                      where dep.DepartmentId == department_id
                                      select dep).FirstOrDefault();
            
            if (department == null)
                throw new DataNotFoundException("No department found with provided ID");
            else
                return department;
        } catch {
            throw;
        }
    }

    public List<Department> getByUser(long user_id) {
        try {
            long[] department_ids = (from dm in _context.DepartmentMember
                                     where dm.MemberId == user_id
                                     select dm.DepartmentId).ToArray();

            List<Department> departments = (from dep in _context.Department
                                            where department_ids.Contains(dep.DepartmentId)
                                            select dep).ToList();

            if (!departments.Any())
                throw new DataNotFoundException("User is not part of any department");
            
            return departments;
        }
        catch {
            throw;
        }
    }
}
