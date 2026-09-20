using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class VehicleDriver
{
    public Guid VehicleDriverId { get; set; }

    public Guid VehicleId { get; set; }

    public Guid DriverUserId { get; set; }

    public ulong IsPrimary { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual DriverProfile DriverUser { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
