using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class DriverProfile
{
    public Guid UserId { get; set; }

    public string LicenseNumber { get; set; } = null!;

    public string? LicenseType { get; set; }

    public DateOnly? LicenseIssuedDate { get; set; }

    public DateOnly? LicenseExpiryDate { get; set; }

    public ulong IsVerified { get; set; }

    public Guid? VerifiedBy { get; set; }

    public DateTime? VerifiedDate { get; set; }

    public string? VerificationNote { get; set; }

    public ulong IsAvailable { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual ICollection<DriverDocument> DriverDocuments { get; set; } = new List<DriverDocument>();

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<VehicleDriver> VehicleDrivers { get; set; } = new List<VehicleDriver>();

    public virtual User? VerifiedByNavigation { get; set; }
}
