using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class ExpertDatum
{
    public int Id { get; set; }

    public string? Uuid { get; set; }

    public string? ExpertNames { get; set; }

    public string? SeviceUnit { get; set; }

    public int? PositionId { get; set; }

    public string? Speciality { get; set; }

    public string? SpecialityCategory { get; set; }

    public string? PhoneNumber { get; set; }

    public string? TelNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Remark { get; set; }

    public string? Experience { get; set; }
}
