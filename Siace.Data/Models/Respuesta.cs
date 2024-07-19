using System;
using System.Collections.Generic;

namespace Siace.Data.Models;

public partial class Respuesta
{
    public int ResId { get; set; }

    public int ResPreId { get; set; }

    public string? ResValor { get; set; }

    public int? ResValorEvaluacion { get; set; }

    public virtual ICollection<ContestacionesRespuesta> ContestacionesRespuesta { get; set; } = new List<ContestacionesRespuesta>();

    public virtual Pregunta ResPre { get; set; } = null!;
}
