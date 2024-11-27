using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditDetailInfo
{
    public int Id { get; set; }

    public string? Uuid { get; set; }

    public int? AuditBasicId { get; set; }

    public int? EnterTypeId { get; set; }

    public int? MeetingTypeId { get; set; }

    public DateOnly? AuditStartDate { get; set; }

    public DateOnly? AuditEndDate { get; set; }

    public string? Filename { get; set; }

    public string? FilenameUuid { get; set; }

    public string? Reportname { get; set; }

    public string? ReportnameUuid { get; set; }
}
