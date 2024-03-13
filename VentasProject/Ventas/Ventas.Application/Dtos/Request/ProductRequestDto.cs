using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Application.Dtos.Request
{
    public class ProductRequestDto
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public decimal? Precio { get; set; }

        public string? Descripcion { get; set; }

        public int? Cantidad { get; set; }
    }
}
