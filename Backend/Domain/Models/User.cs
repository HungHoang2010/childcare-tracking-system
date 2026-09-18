using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public ulong IsVerified { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? LockoutEndDate { get; set; }

    public int FailedLoginCount { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual ICollection<UserSchool> UserSchools { get; set; } = new List<UserSchool>();
}
