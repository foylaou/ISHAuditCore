using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditSuggest
{
    public string Uuid { get; set; } = null!;

    public int? AuditBasicId { get; set; }

    public int? AuditDetailId { get; set; }

    public int? SuggestItemId { get; set; }

    public int? ParentId { get; set; }

    public int? ItemNo { get; set; }

    public string? Suggest { get; set; }

    public string? SubSuggest { get; set; }

    public string? Participate { get; set; }

    public string? Action { get; set; }

    public string? ShortTerm { get; set; }

    public string? MidTerm { get; set; }

    public string? LongTerm { get; set; }

    public string? HandlingSituation { get; set; }

    public string? ImproveStatus { get; set; }

    public string? ResponsibleUnit { get; set; }

    public string? Budget { get; set; }

    public int? BudgetVal { get; set; }

    public int? ExpertId { get; set; }

    public string? Remarks { get; set; }

    public DateTime? CreateBy { get; set; }

    public string? AiSuggest { get; set; }
}
