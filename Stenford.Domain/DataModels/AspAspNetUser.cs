using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stenford.Domain.DataModels;

[Table("ASP_AspNetUser")]
public partial class AspAspNetUser
{
    [Key]
    [Column("AspNetUserID")]
    public Guid AspNetUserId { get; set; }

    [StringLength(200)]
    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

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

    [InverseProperty("AspNetUser")]
    public virtual ICollection<AspAspNetUserWiseRole> AspAspNetUserWiseRoles { get; set; } = new List<AspAspNetUserWiseRole>();

    [InverseProperty("AspNetUser")]
    public virtual ICollection<SecAdmin> SecAdmins { get; set; } = new List<SecAdmin>();

    [InverseProperty("AspNetUser")]
    public virtual ICollection<SecSalesPerson> SecSalesPeople { get; set; } = new List<SecSalesPerson>();
}
