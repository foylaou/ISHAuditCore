using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class TownshipInfo
{
    public int Id { get; set; }

    public string? Township { get; set; }

    public int? CityId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
