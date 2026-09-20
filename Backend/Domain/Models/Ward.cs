using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Ward
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? NameEn { get; set; }

    public string? FullName { get; set; }

    public string? FullNameEn { get; set; }

    public string? CodeName { get; set; }

    public string? PostalCode { get; set; }

    public string? ProvinceCode { get; set; }

    public int? AdministrativeUnitId { get; set; }

    public virtual AdministrativeUnit? AdministrativeUnit { get; set; }

    public virtual Province? ProvinceCodeNavigation { get; set; }

    public virtual ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();

    public virtual ICollection<School> Schools { get; set; } = new List<School>();

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
