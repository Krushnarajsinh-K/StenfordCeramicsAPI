using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stenford.Domain.DataModels;

[Table("ASP_AspNetUserWiseRole")]
public partial class AspAspNetUserWiseRole
{
    [Key]
    [Column("AspNetUserWiseRoleID")]
    public int AspNetUserWiseRoleId { get; set; }

    [Column("AspNetUserID")]
    public Guid AspNetUserId { get; set; }

    [Column("AspNetUserRoleID")]
    public int AspNetUserRoleId { get; set; }

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
    [InverseProperty("AspAspNetUserWiseRoles")]
    public virtual AspAspNetUser AspNetUser { get; set; } = null!;

    [ForeignKey("AspNetUserRoleId")]
    [InverseProperty("AspAspNetUserWiseRoles")]
    public virtual AspAspNetUserRole AspNetUserRole { get; set; } = null!;
}
