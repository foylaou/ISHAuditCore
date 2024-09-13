using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

public partial class credential
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public byte[] Descriptor { get; set; } = null!;

    public byte[] PublicKey { get; set; } = null!;

    public int SignatureCounter { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("credentials")]
    public virtual user_info User { get; set; } = null!;
}
