using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Rolespermission
{
    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public string? UpdateBy { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public ulong? IsDeleted { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
