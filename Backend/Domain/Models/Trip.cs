using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Trip
{
    public Guid TripId { get; set; }

    public Guid SchoolId { get; set; }

    public Guid RouteId { get; set; }

    public Guid VehicleId { get; set; }

    public Guid? DriverUserId { get; set; }

    public DateOnly TripDate { get; set; }

    public string TripType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public TimeOnly? ScheduledStartTime { get; set; }

    public DateTime? ActualStartTime { get; set; }

    public TimeOnly? ScheduledArrivalTime { get; set; }

    public DateTime? ActualArrivalTime { get; set; }

    public string? Note { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual DriverProfile? DriverUser { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual School School { get; set; } = null!;

    public virtual ICollection<TripStudent> TripStudents { get; set; } = new List<TripStudent>();

    public virtual Vehicle Vehicle { get; set; } = null!;
}
