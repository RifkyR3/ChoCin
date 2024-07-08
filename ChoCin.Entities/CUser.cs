using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChoCin.Entities;

[Table("c_user")]
public partial class CUser
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [StringLength(100)]
    public string UserPassword { get; set; } = null!;

    [StringLength(255)]
    public string? UserFullName { get; set; }
}
