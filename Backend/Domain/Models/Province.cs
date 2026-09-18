using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Province
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? NameEn { get; set; }

    public string FullName { get; set; } = null!;

    public string? FullNameEn { get; set; }

    public string? CodeName { get; set; }

    public string? PostalCodePrefix { get; set; }

    public int? AdministrativeUnitId { get; set; }

    public virtual AdministrativeUnit? AdministrativeUnit { get; set; }

    public virtual ICollection<School> Schools { get; set; } = new List<School>();

    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}
