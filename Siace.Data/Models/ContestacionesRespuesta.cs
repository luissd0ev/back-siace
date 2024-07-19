using System;
using System.Collections.Generic;

namespace Siace.Data.Models;

public partial class ContestacionesRespuesta
{
    public int CorId { get; set; }

    public int CorConId { get; set; }

    public int? CorResId { get; set; }

    public int CorPreId { get; set; }

    public string? CorValor { get; set; }

    public byte[]? CorImagen { get; set; }

    public bool? CorNoContesto { get; set; }

    public virtual Contestacione CorCon { get; set; } = null!;

    public virtual Pregunta CorPre { get; set; } = null!;

    public virtual Respuesta? CorRes { get; set; }
}
