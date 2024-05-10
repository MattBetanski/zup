using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class ProjectService {
    private readonly ZupContext _context;

    public ProjectService(ZupContext context) {
        _context = context;
    }

    public Project Create(Project new_project) {
        try {
            bool project_exists = (from prj in _context.Project
                                   where prj.Name == new_project.Name
                                   select prj).Any();

            if (project_exists)
                throw new ObjectNameInUseException("A project with the provided name already exists within the department");

            _context.Project.Add(new_project);
            _context.SaveChanges();

            return new_project;
        }
        catch {
            throw;
        }
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

    
}