using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("enterprise_name")]
public partial class enterprise_name
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? enterprise { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? code { get; set; }
}
