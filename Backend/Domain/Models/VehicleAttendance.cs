using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class VehicleAttendance
{
    public Guid VehicleAttendanceId { get; set; }

    public Guid TripStudentId { get; set; }

    public string BoardingStatus { get; set; } = null!;

    public DateTime? BoardedAt { get; set; }

    public DateTime? DroppedOffAt { get; set; }

    public string? Note { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public Guid? CreatedBy { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public virtual TripStudent TripStudent { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
