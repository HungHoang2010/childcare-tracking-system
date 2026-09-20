using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class RouteStop
{
    public Guid RouteStopId { get; set; }

    public Guid RouteId { get; set; }

    public string StopCode { get; set; } = null!;

    public string StopName { get; set; } = null!;

    public string StopType { get; set; } = null!;

    public uint StopOrder { get; set; }

    public string AddressDetail { get; set; } = null!;

    public string WardCode { get; set; } = null!;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public TimeOnly? ExpectedArrivalTime { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual ICollection<StudentTransportAssignment> StudentTransportAssignments { get; set; } = new List<StudentTransportAssignment>();

    public virtual ICollection<TripStudent> TripStudents { get; set; } = new List<TripStudent>();

    public virtual Ward WardCodeNavigation { get; set; } = null!;
}
