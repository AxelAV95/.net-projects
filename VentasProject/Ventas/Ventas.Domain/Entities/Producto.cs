using System;
using System.Collections.Generic;

namespace Ventas.Domain.Entities;

public partial class Producto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public string? Descripcion { get; set; }

    public int? Cantidad { get; set; }
}
