using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("review_queue")]
public partial class review_queue
{
    [Key]
    public int id { get; set; }

    /// <summary>
    /// 被審核人員名稱
    /// 
    /// </summary>
    [StringLength(50)]
    public string? username { get; set; }

    /// <summary>
    /// 被審核人員電子郵件
    /// 
    /// </summary>
    [StringLength(255)]
    [Unicode(false)]
    public string email { get; set; } = null!;

    /// <summary>
    /// 被審核人員電話
    /// </summary>
    [StringLength(20)]
    [Unicode(false)]
    public string? phone { get; set; }

    /// <summary>
    /// 驗證碼or連結
    /// 
    /// </summary>
    [StringLength(50)]
    [Unicode(false)]
    public string? verification_code { get; set; }

    /// <summary>
    /// 驗證碼存在時間
    /// 
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? approval_time { get; set; }

    /// <summary>
    /// email/phone審核狀態(0:未審核, 1:審核通過, 2:審核不通過)
    /// 
    /// </summary>
    public byte? verification_status { get; set; }

    /// <summary>
    /// 哪一間企業
    /// 
    /// </summary>
    public int? enterprise_id { get; set; }

    /// <summary>
    /// 哪一間公司
    /// 
    /// </summary>
    public int? company_id { get; set; }

    /// <summary>
    /// 哪一間工廠
    /// 
    /// </summary>
    public int? factory_id { get; set; }

    /// <summary>
    /// 其單位使用網域
    /// </summary>
    [StringLength(255)]
    [Unicode(false)]
    public string? enterprise_domain { get; set; }

    /// <summary>
    /// 人員審核狀態(0:未審核, 1:審核通過, 2:審核不通過)
    /// </summary>
    public byte? status { get; set; }

    /// <summary>
    /// 審核時間
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? createdAt { get; set; }

    /// <summary>
    /// 審核者
    /// 
    /// </summary>
    public int? approvedBy { get; set; }
}
