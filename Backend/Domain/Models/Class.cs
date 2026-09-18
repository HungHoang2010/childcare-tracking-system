using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Class
{
    public Guid ClassId { get; set; }

    public Guid AcademicYearId { get; set; }

    public string ClassName { get; set; } = null!;

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual AcademicYear AcademicYear { get; set; } = null!;
}
