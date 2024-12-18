﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("user_info")]
public partial class user_info
{
    [Key]
    public int id { get; set; }

    [StringLength(25)]
    public string? username { get; set; }

    public string? password { get; set; }

    [StringLength(50)]
    public string? nickname { get; set; }

    public int? enterprise_id { get; set; }

    public int? company_id { get; set; }

    public int? factory_id { get; set; }

    public string? authority { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }

    [StringLength(50)]
    public string? anyllmworkspace { get; set; }

    [Unicode(false)]
    public string? avatar { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? mobile { get; set; }

    [Unicode(false)]
    public string? email { get; set; }

    [MaxLength(32)]
    public byte[]? salt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<credential> credentials { get; set; } = new List<credential>();
}
