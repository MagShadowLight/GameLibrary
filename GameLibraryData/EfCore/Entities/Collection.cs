using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryData.EfCore.Entities;

public partial class Collection
{
    [Key]
    public int CollectionId { get; set; }

    public int UserId { get; set; }

    public int GameId { get; set; }

    public DateTime DateLastPlayed { get; set; }

    public int TimesPlayed { get; set; }

    [ForeignKey("GameId")]
    [InverseProperty("Collections")]
    public virtual Game Game { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Collections")]
    public virtual User User { get; set; } = null!;
}
