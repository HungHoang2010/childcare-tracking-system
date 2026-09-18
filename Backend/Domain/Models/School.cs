using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class School
{
    public Guid SchoolId { get; set; }

    public string SchoolCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? ShortName { get; set; }

    public string SchoolType { get; set; } = null!;

    public string? LogoUrl { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string AddressDetail { get; set; } = null!;

    public string ProvinceId { get; set; } = null!;

    public string WardId { get; set; } = null!;

    public string? PostalCode { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public string TimeZone { get; set; } = null!;

    public string? UpdateBy { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public ulong? IsDeleted { get; set; }

    public virtual ICollection<AcademicYear> AcademicYears { get; set; } = new List<AcademicYear>();

    public virtual Province Province { get; set; } = null!;

    public virtual ICollection<UserSchool> UserSchools { get; set; } = new List<UserSchool>();

    public virtual Ward Ward { get; set; } = null!;
}
