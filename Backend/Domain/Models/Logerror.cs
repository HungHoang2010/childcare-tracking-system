using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Logerror
{
    public long Id { get; set; }

    public DateTime CreateDate { get; set; }

    public string? LogContent { get; set; }

    public int? StatusId { get; set; }

    public string? ProcessContent { get; set; }

    public int TypeLog { get; set; }

    public string? CreateBy { get; set; }
}
