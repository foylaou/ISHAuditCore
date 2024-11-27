using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("audit_cause")]
public partial class audit_cause
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? cause_name { get; set; }

    [StringLength(50)]
    public string? cause_code { get; set; }
}
