using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Logactivity
{
    public long Id { get; set; }

    public DateTime CreateDate { get; set; }

    public string? Ip { get; set; }

    public string? ObjectGuid { get; set; }

    public string? LogContent { get; set; }

    public Guid? UserId { get; set; }
}
