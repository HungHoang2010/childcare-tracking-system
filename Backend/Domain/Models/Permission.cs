using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Permission
{
    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public string PermissionDescription { get; set; } = null!;

    public string? UpdateBy { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public ulong? IsAccepted { get; set; }

    public ulong? IsDeleted { get; set; }

    public virtual ICollection<Rolespermission> Rolespermissions { get; set; } = new List<Rolespermission>();
}
