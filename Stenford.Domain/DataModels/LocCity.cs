using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stenford.Domain.DataModels;

[Table("LOC_City")]
public partial class LocCity
{
    [Key]
    [Column("CityID")]
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    [Column("StateID")]
    public int StateId { get; set; }

    public string? StateCode { get; set; }

    [Column("CountryID")]
    public int CountryId { get; set; }

    public string? CountryCode { get; set; }

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
    [InverseProperty("LocCities")]
    public virtual LocCountry Country { get; set; } = null!;

    [InverseProperty("City")]
    public virtual ICollection<SecSalesPerson> SecSalesPeople { get; set; } = new List<SecSalesPerson>();

    [InverseProperty("City")]
    public virtual ICollection<ShoShowroom> ShoShowrooms { get; set; } = new List<ShoShowroom>();

    [ForeignKey("StateId")]
    [InverseProperty("LocCities")]
    public virtual LocState State { get; set; } = null!;
}
