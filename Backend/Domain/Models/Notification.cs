using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Notification
{
    public Guid NotificationId { get; set; }

    public Guid SchoolId { get; set; }

    public Guid UserId { get; set; }

    public string NotificationType { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Message { get; set; }

    public string Priority { get; set; } = null!;

    public Guid? ReferenceId { get; set; }

    public string? ReferenceType { get; set; }

    public ulong IsRead { get; set; }

    public DateTime? ReadAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public ulong IsDeleted { get; set; }

    public virtual School School { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
