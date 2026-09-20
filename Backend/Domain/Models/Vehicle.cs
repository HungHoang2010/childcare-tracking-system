using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Vehicle
{
    public Guid VehicleId { get; set; }

    public Guid SchoolId { get; set; }

    public string VehicleCode { get; set; } = null!;

    public string PlateNumber { get; set; } = null!;

    public string? VehicleName { get; set; }

    public string? VehicleType { get; set; }

    public ushort Capacity { get; set; }

    public short? ManufactureYear { get; set; }

    public DateOnly? RegistrationExpiryDate { get; set; }

    public DateOnly? InspectionExpiryDate { get; set; }

    public string Status { get; set; } = null!;

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual School School { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

    public virtual ICollection<VehicleDriver> VehicleDrivers { get; set; } = new List<VehicleDriver>();
}
