using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class SchoolAttendance
{
    public Guid SchoolAttendanceId { get; set; }

    public Guid SchoolId { get; set; }

    public Guid StudentId { get; set; }

    public Guid AcademicYearId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public string AttendanceStatus { get; set; } = null!;

    public DateTime? CheckedInAt { get; set; }

    public string? Reason { get; set; }

    public Guid? CheckedBy { get; set; }

    public Guid? CreateBy { get; set; }

    public Guid? UpdateBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual AcademicYear AcademicYear { get; set; } = null!;

    public virtual User? CheckedByNavigation { get; set; }

    public virtual School School { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
