using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class TripStudent
{
    public Guid TripStudentId { get; set; }

    public Guid TripId { get; set; }

    public Guid StudentId { get; set; }

    public Guid? RouteStopId { get; set; }

    public ulong IsExpected { get; set; }

    public string? Note { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual RouteStop? RouteStop { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Trip Trip { get; set; } = null!;

    public virtual VehicleAttendance? VehicleAttendance { get; set; }
}
