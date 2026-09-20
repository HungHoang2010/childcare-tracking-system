using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class AcademicYear
{
    public Guid AcademicYearId { get; set; }

    public Guid SchoolId { get; set; }

    public string YearCode { get; set; } = null!;

    public string YearName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public ulong IsCurrent { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual School School { get; set; } = null!;

    public virtual ICollection<SchoolAttendance> SchoolAttendances { get; set; } = new List<SchoolAttendance>();
}
