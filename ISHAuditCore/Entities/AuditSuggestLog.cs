using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditSuggestLog
{
    public int Id { get; set; }

    public string? SuggestUuid { get; set; }

    public string? ColName { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public DateTime? ChangeDate { get; set; }
}
