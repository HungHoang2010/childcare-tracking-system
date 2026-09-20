using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class StudentTransportAssignment
{
    public Guid AssignmentId { get; set; }

    public Guid StudentId { get; set; }

    public Guid RouteStopId { get; set; }

    public string TransportType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual RouteStop RouteStop { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
