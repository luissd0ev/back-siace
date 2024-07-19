using System;
using System.Collections.Generic;

namespace Siace.Data.Models;

public partial class TiposPreguntasRespuesta
{
    public int TprId { get; set; }

    public int TprTipId { get; set; }

    public string TprRespuesta { get; set; } = null!;

    public int? TprValorEvaluacion { get; set; }

    public virtual TiposPregunta TprTip { get; set; } = null!;
}
