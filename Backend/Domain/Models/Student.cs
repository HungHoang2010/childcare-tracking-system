using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Student
{
    public Guid StudentId { get; set; }

    public Guid ClassId { get; set; }

    public string StudentCode { get; set; } = null!;

    public string StudentName { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string AvatarUrl { get; set; } = null!;

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<ParentStudent> ParentStudents { get; set; } = new List<ParentStudent>();

    public virtual ICollection<SchoolAttendance> SchoolAttendances { get; set; } = new List<SchoolAttendance>();

    public virtual ICollection<StudentTransportAssignment> StudentTransportAssignments { get; set; } = new List<StudentTransportAssignment>();

    public virtual ICollection<TripStudent> TripStudents { get; set; } = new List<TripStudent>();
}
