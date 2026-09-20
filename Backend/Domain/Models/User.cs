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

    public virtual ICollection<DriverDocument> DriverDocuments { get; set; } = new List<DriverDocument>();

    public virtual DriverProfile? DriverProfileUser { get; set; }

    public virtual ICollection<DriverProfile> DriverProfileVerifiedByNavigations { get; set; } = new List<DriverProfile>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ParentProfile? ParentProfile { get; set; }

    public virtual ICollection<SchoolAttendance> SchoolAttendances { get; set; } = new List<SchoolAttendance>();

    public virtual ICollection<UserSchool> UserSchools { get; set; } = new List<UserSchool>();

    public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();

    public virtual ICollection<VehicleAttendance> VehicleAttendances { get; set; } = new List<VehicleAttendance>();
}
