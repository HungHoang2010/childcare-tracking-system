using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class ParentStudent
{
    public Guid ParentStudentId { get; set; }

    public Guid ParentUserId { get; set; }

    public Guid StudentId { get; set; }

    public string Relationship { get; set; } = null!;

    public ulong IsPrimaryContact { get; set; }

    public ulong CanReceiveNotification { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual ParentProfile ParentUser { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
