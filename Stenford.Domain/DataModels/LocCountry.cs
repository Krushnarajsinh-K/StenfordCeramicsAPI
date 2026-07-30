using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stenford.Domain.DataModels;

[Table("LOC_Country")]
public partial class LocCountry
{
    [Key]
    [Column("CountryID")]
    public int CountryId { get; set; }

    [StringLength(255)]
    public string CountryName { get; set; } = null!;

    [StringLength(3)]
    public string? Iso3 { get; set; }

    [StringLength(2)]
    public string? Iso2 { get; set; }

    [StringLength(50)]
    public string? PhoneCode { get; set; }

    [StringLength(255)]
    public string? Capital { get; set; }

    [StringLength(100)]
    public string? Currency { get; set; }

    [StringLength(50)]
    public string? CurrencySymbol { get; set; }

    [StringLength(20)]
    public string? Tld { get; set; }

    public string? Native { get; set; }

    [StringLength(100)]
    public string? Region { get; set; }

    [StringLength(100)]
    public string? SubRegion { get; set; }

    public string? TimeZones { get; set; }

    [Precision(10, 8)]
    public decimal? Latitude { get; set; }

    [Precision(11, 8)]
    public decimal? Longitude { get; set; }

    [StringLength(20)]
    public string? Emoji { get; set; }

    public string? EmojiU { get; set; }

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

    [InverseProperty("Country")]
    public virtual ICollection<LocCity> LocCities { get; set; } = new List<LocCity>();

    [InverseProperty("Country")]
    public virtual ICollection<LocState> LocStates { get; set; } = new List<LocState>();

    [InverseProperty("Country")]
    public virtual ICollection<SecSalesPerson> SecSalesPeople { get; set; } = new List<SecSalesPerson>();

    [InverseProperty("Country")]
    public virtual ICollection<ShoShowroom> ShoShowrooms { get; set; } = new List<ShoShowroom>();
}
