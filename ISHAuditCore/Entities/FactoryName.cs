using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class FactoryName
{
    public int Id { get; set; }

    public string? Factory { get; set; }

    public int? CompanyId { get; set; }

    public int? IndustrialAreaId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
