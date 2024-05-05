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
                            where proj.ProjectId == id
                            select proj).FirstOrDefault();
        return project;
    }

    public Project Create(Project new_project) {
        _context.Project.Add(new_project);
        _context.SaveChanges();

        return new_project;
    }
}