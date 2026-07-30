using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stenford.Domain.DataModels;

[Table("ASP_AspNetUserRole")]
[Index("AspNetUserRole", Name = "ASP_AspNetUserRole_AspNetUserRole_key", IsUnique = true)]
public partial class AspAspNetUserRole
{
    [Key]
    [Column("AspNetUserRoleID")]
    public int AspNetUserRoleId { get; set; }

    [StringLength(200)]
    public string AspNetUserRole { get; set; } = null!;

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

    [InverseProperty("AspNetUserRole")]
    public virtual ICollection<AspAspNetUserWiseRole> AspAspNetUserWiseRoles { get; set; } = new List<AspAspNetUserWiseRole>();

    [InverseProperty("Role")]
    public virtual ICollection<SecAdmin> SecAdmins { get; set; } = new List<SecAdmin>();
}
