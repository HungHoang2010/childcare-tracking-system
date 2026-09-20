using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class DriverDocument
{
    public Guid DriverDocumentId { get; set; }

    public Guid DriverUserId { get; set; }

    public string DocumentType { get; set; } = null!;

    public uint DocumentVersion { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string MimeType { get; set; } = null!;

    public ulong FileSize { get; set; }

    public ulong IsVerified { get; set; }

    public Guid? VerifiedBy { get; set; }

    public DateTime? VerifiedDate { get; set; }

    public string? VerificationNote { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual DriverProfile DriverUser { get; set; } = null!;

    public virtual User? VerifiedByNavigation { get; set; }
}
