using System;
using System.Collections.Generic;

namespace MangaAPIEF.Entities;

public partial class Tblogerror
{
    public int Logerrorid { get; set; }

    public int? Logerrorseveridad { get; set; }

    public string? Logerrorstoreprocedure { get; set; }

    public int? Logerrornumero { get; set; }

    public string? Logerrordescripcion { get; set; }

    public int? Logerrorlinea { get; set; }

    public DateTime? Logfechahora { get; set; }
}
