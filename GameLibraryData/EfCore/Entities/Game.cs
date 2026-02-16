using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryData.EfCore.Entities;

public partial class Game
{
    [Key]
    public int GameId { get; set; }

    public string Title { get; set; } = null!;

    public string Developer { get; set; } = null!;

    public string Publisher { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public string Genre { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Prices { get; set; }

    [InverseProperty("Game")]
    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
}
