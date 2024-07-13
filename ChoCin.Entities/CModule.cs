using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChoCin.Entities;

[Table("c_module")]
[Index("ModuleSubId", Name = "ModuleSubId")]
public partial class CModule
{
    [Key]
    public int ModuleId { get; set; }

    public int? ModuleSubId { get; set; }

    [StringLength(100)]
    public string ModuleName { get; set; } = null!;

    [StringLength(100)]
    public string? ModuleIcon { get; set; }

    [StringLength(100)]
    public string ModulePath { get; set; } = null!;

    public int ModuleOrder { get; set; }

    [InverseProperty("ModuleSub")]
    public virtual ICollection<CModule> InverseModuleSub { get; set; } = new List<CModule>();

    [ForeignKey("ModuleSubId")]
    [InverseProperty("InverseModuleSub")]
    public virtual CModule? ModuleSub { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("Modules")]
    public virtual ICollection<CGroup> Groups { get; set; } = new List<CGroup>();
}
