using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class ProjectService {
    private readonly ZupContext _context;

    public ProjectService(ZupContext context) {
        _context = context;
    }

    public IEnumerable<Project> GetAll() {
        return _context.Project
                .AsNoTracking()
                .ToList();
    }

    public Project? GetById(int id) {
        Project? project = (from proj in _context.Project
                            where proj.project_id == id
                            select proj).FirstOrDefault();
        return project;
    }
}