using System;
using System.Collections.Generic;

namespace RestApiTienda.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string? Talla { get; set; }

    public string? Color { get; set; }

    public decimal? Precio { get; set; }

    public string? Descripcion { get; set; }
}
