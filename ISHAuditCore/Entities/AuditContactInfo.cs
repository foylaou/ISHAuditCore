using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditContactInfo
{
    public int Id { get; set; }

    public string? Uuid { get; set; }

    public string? UserName { get; set; }

    public string? UserDepartment { get; set; }

    public string? UserJobTitle { get; set; }

    public string? UserMail { get; set; }

    public string? UserPhone { get; set; }

    public int? EnterpriseId { get; set; }

    public int? CompanyId { get; set; }

    public int? FactoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }
}
