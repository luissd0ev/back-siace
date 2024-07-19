using System;
using System.Collections.Generic;

namespace Siace.Data.Models;

public partial class Pregunta
{
    public int PreId { get; set; }

    public string PrePregunta { get; set; } = null!;

    public bool PreActivo { get; set; }

    public int PreTicId { get; set; }

    public int PreEncId { get; set; }

    public string? PreTipoDato { get; set; }

    public string? PreRangoIni { get; set; }

    public string? PreRangoFin { get; set; }

    public int PrePilId { get; set; }

    public int? PrePreIdTrigger { get; set; }

    public int? PreResIdTrigger { get; set; }

    public string? PreClaveCompuesta { get; set; }

    public int? PreNoSabe { get; set; }

    public bool? PreValoracion { get; set; }

    public int? PreTipId { get; set; }

    public virtual ICollection<ContestacionesRespuesta> ContestacionesRespuesta { get; set; } = new List<ContestacionesRespuesta>();

    public virtual Encuesta PreEnc { get; set; } = null!;

    public virtual Pilare PrePil { get; set; } = null!;

    public virtual TiposPregunta? PreTip { get; set; }

    public virtual ICollection<Respuesta> Respuesta { get; set; } = new List<Respuesta>();
}
