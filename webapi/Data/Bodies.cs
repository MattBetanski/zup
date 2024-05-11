using webapi.Models;

namespace webapi.Data;

public class DepartmentBody {
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Visibility { get; set; }
}

public class ItemBody {
    public required string Name { get; set; }
    public long? ParentId { get; set; }
    public string? Description { get; set; }
    public required Models.Type Type { get; set; }
    public State State { get; set; }
    public required long ProjectId { get; set; }
}

public class ItemBodyU {
    public required string Name { get; set; }
    public long? ParentId { get; set; }
    public string? Description { get; set; }
    public State State { get; set; }
}

public class ItemCommentBody {
    public required string Comment { get; set; }
}

public class LoginBody {
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class ProjectBody {
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required long DepartmentId { get; set; }
}

public class RoleBody {
    public required long DepartmentId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required RoleLevel ItemLevel { get; set; }
    public required RoleLevel WikiLevel { get; set; }
    public required bool WikiDelete { get; set; }
}

public class WikiBody {
    public required string Title { get; set; }
    public string? Content { get; set; }
}

public class UserBody {
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
