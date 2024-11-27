using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditBasicInfo
{
    public int Id { get; set; }

    public string? Uuid { get; set; }

    public int? FactoryId { get; set; }

    public int? AuditTypeId { get; set; }

    public int? AuditCauseId { get; set; }

    public string? DisaterTypesId { get; set; }

    public string? DisaterTypes { get; set; }

    public DateTime? IncidentDatetime { get; set; }

    public string? IncidentDescription { get; set; }

    public string? Sd { get; set; }

    public string? Penalty { get; set; }

    public string? PenaltyDetail { get; set; }

    public string? ImproveStatus { get; set; }

    public string? Situation { get; set; }

    public decimal? ParticipateStatusVal { get; set; }

    public decimal? ImproveStatusVal { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
