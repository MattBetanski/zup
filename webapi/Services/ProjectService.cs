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

    public Project GetById(long id) {
        try {
            Project project = (from proj in _context.Project
                                where proj.ProjectId == id
                                select proj).FirstOrDefault() ?? throw new DataNotFoundException("The selected project could not be found");
            return project;
        }
        catch {
            throw;
        }
    }

    public void AssignUserRole(ProjectUserRole projectUserRole) {
        try {
            bool userHasRole = (from pur in _context.ProjectUserRole
                                where pur.UserId == projectUserRole.UserId
                                select pur).Any();
            if(userHasRole)
                throw new UserAlreadyInGroupException("Selected user has already been assigned a role");
            
            _context.ProjectUserRole.Add(projectUserRole);
            _context.SaveChanges();
        }
        catch {
            throw;
        }
    }

    public void UpdateUserRole(long project_id, long user_id, long role_id) {
        try {
            ProjectUserRole existingProjectUserRole = (from pur in _context.ProjectUserRole
                                                       where pur.ProjectId == project_id && pur.UserId == user_id
                                                       select pur).FirstOrDefault() ?? throw new DataNotFoundException("The selected project member could not be found");

            existingProjectUserRole.RoleId = role_id;
            _context.SaveChanges();
        }
        catch {
            throw;
        }
    }

    public Department GetDepartment(long project_id) {
        try {
            Project? project = (from prj in _context.Project
                                where prj.ProjectId == project_id
                                select prj).FirstOrDefault() ?? throw new DataNotFoundException("The selected project could not be found");
            
            Department? department = (from dep in _context.Department
                                      where dep.DepartmentId == project.DepartmentId
                                      select dep).FirstOrDefault() ?? throw new DataNotFoundException("The selected department could not be found");

            return department;
        }
        catch {
            throw;
        }
    }

    public bool checkIfInProject(long user_id, long project_id) {
        try {
            bool is_project_member = (from pur in _context.ProjectUserRole
                                      where pur.UserId == user_id && pur.ProjectId == project_id
                                      select pur).Any();
            return is_project_member;
        }
        catch {
            throw;
        }
    }
}