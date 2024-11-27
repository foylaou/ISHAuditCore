using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class SuggestCategory
{
    public int Id { get; set; }

    public string? SuggestCategory1 { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
