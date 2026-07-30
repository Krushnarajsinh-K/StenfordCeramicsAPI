using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stenford.Domain.DataModels;

[Table("SEC_Admin")]
public partial class SecAdmin
{
    [Key]
    [Column("AdminID")]
    public int AdminId { get; set; }

    [Column("AspNetUserID")]
    public Guid AspNetUserId { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    [StringLength(200)]
    public string UserName { get; set; } = null!;

    [StringLength(200)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(500)]
    public string? ProfileImage { get; set; }

    public bool IsDeleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime ModifiedAt { get; set; }

    public Guid ModifiedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    [ForeignKey("AspNetUserId")]
    [InverseProperty("SecAdmins")]
    public virtual AspAspNetUser AspNetUser { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("SecAdmins")]
    public virtual AspAspNetUserRole Role { get; set; } = null!;
}
