using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stenford.Domain.DataModels;

[Table("SEC_SalesPerson")]
public partial class SecSalesPerson
{
    [Key]
    [Column("SalesPersonID")]
    public int SalesPersonId { get; set; }

    [Column("AspNetUserID")]
    public Guid AspNetUserId { get; set; }

    [StringLength(256)]
    public string Email { get; set; } = null!;

    [StringLength(256)]
    public string SalesPersonName { get; set; } = null!;

    [StringLength(20)]
    public string PrimaryContact { get; set; } = null!;

    [StringLength(20)]
    public string? SecondaryContact { get; set; }

    public string Address { get; set; } = null!;

    [Column("CountryID")]
    public int CountryId { get; set; }

    [Column("StateID")]
    public int StateId { get; set; }

    [Column("CityID")]
    public int CityId { get; set; }

    public bool IsActive { get; set; }

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

    public string Password { get; set; } = null!;

    [StringLength(256)]
    public string ContactPerson { get; set; } = null!;

    [ForeignKey("AspNetUserId")]
    [InverseProperty("SecSalesPeople")]
    public virtual AspAspNetUser AspNetUser { get; set; } = null!;

    [ForeignKey("CityId")]
    [InverseProperty("SecSalesPeople")]
    public virtual LocCity City { get; set; } = null!;

    [ForeignKey("CountryId")]
    [InverseProperty("SecSalesPeople")]
    public virtual LocCountry Country { get; set; } = null!;

    [ForeignKey("StateId")]
    [InverseProperty("SecSalesPeople")]
    public virtual LocState State { get; set; } = null!;

    [InverseProperty("SalesPerson")]
    public virtual ICollection<VisVisit> VisVisits { get; set; } = new List<VisVisit>();
}
