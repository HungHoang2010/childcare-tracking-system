using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserOtp
{
    public Guid Otpid { get; set; }

    public string Email { get; set; } = null!;

    public string OtpHash { get; set; } = null!;

    public string OtpType { get; set; } = null!;

    public DateTime ExpiredDate { get; set; }

    public ulong IsUsed { get; set; }

    public DateTime? UsedDate { get; set; }

    public byte AttemptCount { get; set; }

    public byte MaxAttempts { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public ulong IsDeleted { get; set; }
}
