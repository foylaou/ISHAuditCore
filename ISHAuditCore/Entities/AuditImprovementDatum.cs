using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditImprovementDatum
{
    public string Uuid { get; set; } = null!;

    public string? PlanNo { get; set; }

    public int? AuditBasicId { get; set; }

    public int? ImproveType { get; set; }

    public string? PlanName { get; set; }

    public string? PlanDesc { get; set; }

    public DateOnly? StartTime { get; set; }

    public DateOnly? EndTime { get; set; }

    public string? HandlingSituation { get; set; }

    public string? ResponsibleUnit { get; set; }

    public int? BudgetVal { get; set; }

    public int? BenefitVal { get; set; }

    public string? BenefitRemark { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
