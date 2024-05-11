using webapi.Models;

namespace webapi.Data;

public class UserAndRoleResponse {
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public long RoleId { get; set; }
    public string? RoleName { get; set; }

}

public class InvitesResponse {
    public required long DepartmentId { get; set; }
    public required long InviteeId { get; set; }
    public required string Email { get; set; }
}

public class MinimalDepartment {
    public required string Name { get; set; }
    public required long DepartmentId { get; set; }
}