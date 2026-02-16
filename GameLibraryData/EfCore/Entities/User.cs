using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryData.EfCore.Entities;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public DateTime DateofBirth { get; set; }

    public string Password { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Bios { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string Email { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
}
