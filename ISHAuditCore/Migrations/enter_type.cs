using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("enter_type")]
public partial class enter_type
{
    [Key]
    public int id { get; set; }

    [Column("enter_type")]
    [StringLength(20)]
    public string? enter_type1 { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }
}
