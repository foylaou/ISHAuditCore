using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class ResultDatum
{
    public int Id { get; set; }

    public int? Year { get; set; }

    public string? ExpertName { get; set; }

    public int? ExpertNameId { get; set; }

    public int? EnterpriseId { get; set; }

    public int? CompanyId { get; set; }

    public int? FactoryId { get; set; }

    public int? ResultSuggestionTypeId { get; set; }

    public string? IndicatorCategory { get; set; }

    public string? SuggestionContent { get; set; }

    public string? Response { get; set; }

    public string? Execution { get; set; }

    public DateTime? EstimatedCompletionDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int? ResultReportId { get; set; }

    public int? KpiItemId { get; set; }

    public string? Number { get; set; }

    public string? Remark { get; set; }
}
