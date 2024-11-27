using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class EnterpriseName
{
    public int Id { get; set; }

    public string? Enterprise { get; set; }

    public string? Code { get; set; }
}
