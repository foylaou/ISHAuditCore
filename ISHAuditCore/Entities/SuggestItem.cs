using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class SuggestItem
{
    public int Id { get; set; }

    public string? SuggestItem1 { get; set; }

    public int? SuggestTypeId { get; set; }
}
