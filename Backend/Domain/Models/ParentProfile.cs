using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class ParentProfile
{
    public Guid UserId { get; set; }

    public string? Occupation { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactPhone { get; set; }

    public string? EmergencyContactRelationship { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual ICollection<ParentStudent> ParentStudents { get; set; } = new List<ParentStudent>();

    public virtual User User { get; set; } = null!;
}
