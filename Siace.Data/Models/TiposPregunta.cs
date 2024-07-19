using System;
using System.Collections.Generic;

namespace Siace.Data.Models;

public partial class TiposPregunta
{
    public int TipId { get; set; }

    public int TipTicId { get; set; }

    public string TipDescripcion { get; set; } = null!;

    public bool? TipValoracion { get; set; }

    public virtual ICollection<Pregunta> Pregunta { get; set; } = new List<Pregunta>();

    public virtual TiposControle TipTic { get; set; } = null!;

    public virtual ICollection<TiposPreguntasRespuesta> TiposPreguntasRespuesta { get; set; } = new List<TiposPreguntasRespuesta>();
}
