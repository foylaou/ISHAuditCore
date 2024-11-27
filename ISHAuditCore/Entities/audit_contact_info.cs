using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("audit_contact_info")]
public partial class audit_contact_info
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? uuid { get; set; }

    [StringLength(20)]
    public string? user_name { get; set; }

    [StringLength(50)]
    public string? user_department { get; set; }

    [StringLength(20)]
    public string? user_job_title { get; set; }

    [StringLength(50)]
    public string? user_mail { get; set; }

    [StringLength(20)]
    public string? user_phone { get; set; }

    public int? enterprise_id { get; set; }

    public int? company_id { get; set; }

    public int? factory_id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? created_at { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updated_at { get; set; }

    [StringLength(1)]
    public string? status { get; set; }
}
