using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditControlYuan
{
    public int Id { get; set; }

    public int? AuditBasicId { get; set; }

    public string? Description { get; set; }

    public DateOnly? ComeDate { get; set; }

    public string? ComeDocUuid { get; set; }

    public string? ComeDocText { get; set; }

    public string? ComeDocType { get; set; }

    public DateOnly? BackDate { get; set; }

    public string? BackDocUuid { get; set; }

    public string? BackDocText { get; set; }

    public string? BackDocType { get; set; }

    public string? Remark { get; set; }
}
