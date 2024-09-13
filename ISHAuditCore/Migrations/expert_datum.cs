using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

public partial class expert_datum
{
    [Key]
    public int id { get; set; }

    [StringLength(10)]
    public string? uuid { get; set; }

    [StringLength(255)]
    public string? expert_names { get; set; }

    [StringLength(255)]
    public string? sevice_unit { get; set; }

    public int? position_id { get; set; }

    public string? speciality { get; set; }

    [StringLength(255)]
    public string? speciality_category { get; set; }

    [StringLength(255)]
    public string? phone_number { get; set; }

    [StringLength(255)]
    public string? tel_number { get; set; }

    [StringLength(255)]
    public string? email { get; set; }

    [StringLength(255)]
    public string? address { get; set; }

    [StringLength(255)]
    public string? remark { get; set; }

    [StringLength(255)]
    public string? experience { get; set; }
}
