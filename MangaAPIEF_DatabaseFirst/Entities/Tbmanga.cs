using System;
using System.Collections.Generic;

namespace MangaAPIEF.Entities;

public partial class Tbmanga
{
    public int Mangaid { get; set; }

    public string Manganombre { get; set; } = null!;

    public int Mangatomo { get; set; }

    public DateTime Mangaregistro { get; set; }
}
