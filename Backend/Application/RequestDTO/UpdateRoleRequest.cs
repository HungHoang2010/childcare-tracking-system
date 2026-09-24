using System;

namespace Application.RequestDTO;

public class UpdateRoleRequest
{
    public string RoleName { get; set; } = string.Empty;
    public string RoleDescription { get; set; } = string.Empty;
}
