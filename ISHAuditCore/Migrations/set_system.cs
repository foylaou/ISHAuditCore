using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("set_system")]
public partial class set_system
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? code { get; set; }

    public string? code_val { get; set; }
}
