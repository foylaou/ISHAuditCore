using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class CityInfo
{
    public int Id { get; set; }

    public string? City { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
