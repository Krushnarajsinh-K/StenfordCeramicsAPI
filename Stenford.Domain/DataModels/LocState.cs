using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stenford.Domain.DataModels;

[Table("LOC_State")]
public partial class LocState
{
    [Key]
    [Column("StateID")]
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    [Column("CountryID")]
    public int CountryId { get; set; }

    public string? CountryCode { get; set; }

    public string? StateCode { get; set; }

    [Precision(10, 8)]
    public decimal? Latitude { get; set; }

    [Precision(11, 8)]
    public decimal? Longitude { get; set; }

    public bool IsDeleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("LocStates")]
    public virtual LocCountry Country { get; set; } = null!;

    [InverseProperty("State")]
    public virtual ICollection<LocCity> LocCities { get; set; } = new List<LocCity>();

    [InverseProperty("State")]
    public virtual ICollection<SecSalesPerson> SecSalesPeople { get; set; } = new List<SecSalesPerson>();

    [InverseProperty("State")]
    public virtual ICollection<ShoShowroom> ShoShowrooms { get; set; } = new List<ShoShowroom>();
}
