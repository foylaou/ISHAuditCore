using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class SuggestType
{
    public int Id { get; set; }

    public string? SuggestType1 { get; set; }

    public int? SuggestCategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
