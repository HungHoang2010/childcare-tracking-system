using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserProfile
{
    public Guid UserId { get; set; }

    public string? ResidentNumber { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public ulong? Gender { get; set; }

    public string? Phone { get; set; }

    public string? WardId { get; set; }

    public string? AddressDetail { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Ward? Ward { get; set; }
}
