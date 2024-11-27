using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class CompanyName
{
    public int Id { get; set; }

    public string? Company { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int? EnterpriseId { get; set; }
}
