using System;
using System.Collections.Generic;

namespace GameStore.Data.Models;

public partial class Game
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
