using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserToken
{
    public Guid TokenId { get; set; }

    public Guid UserId { get; set; }

    public string TokenHash { get; set; } = null!;

    public string TokenType { get; set; } = null!;

    public ulong IsRememberMe { get; set; }

    public DateTime ExpiredDate { get; set; }

    public ulong IsRevoked { get; set; }

    public DateTime? RevokedDate { get; set; }

    public string? DeviceName { get; set; }

    public string? Ipaddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public ulong IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}
