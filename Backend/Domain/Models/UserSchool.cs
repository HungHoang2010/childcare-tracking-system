using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserSchool
{
    public Guid UserSchoolId { get; set; }

    public Guid UserId { get; set; }

    public Guid SchoolId { get; set; }

    public Guid RoleId { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual School School { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
