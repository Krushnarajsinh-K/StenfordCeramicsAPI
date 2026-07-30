using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stenford.Domain.DataModels;

[Table("VIS_VisitWiseAttachment")]
public partial class VisVisitWiseAttachment
{
    [Key]
    [Column("VisitAttachmentID")]
    public int VisitAttachmentId { get; set; }

    [Column("VisitID")]
    public int VisitId { get; set; }

    public int AttachmentType { get; set; }

    public string AttachmentPath { get; set; } = null!;

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

    [ForeignKey("VisitId")]
    [InverseProperty("VisVisitWiseAttachments")]
    public virtual VisVisit Visit { get; set; } = null!;
}
