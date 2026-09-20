using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Route
{
    public Guid RouteId { get; set; }

    public Guid SchoolId { get; set; }

    public string RouteCode { get; set; } = null!;

    public string RouteName { get; set; } = null!;

    public string? Description { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();

    public virtual School School { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
