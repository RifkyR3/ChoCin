using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChoCin.Entities;

[Table("c_group")]
public partial class CGroup
{
    [Key]
    public int GroupId { get; set; }

    [StringLength(100)]
    public string GroupName { get; set; } = null!;

    [ForeignKey("GroupId")]
    [InverseProperty("Groups")]
    public virtual ICollection<CModule> Modules { get; set; } = new List<CModule>();

    [ForeignKey("GroupId")]
    [InverseProperty("Groups")]
    public virtual ICollection<CUser> Users { get; set; } = new List<CUser>();
}
