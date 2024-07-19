using System;
using System.Collections.Generic;

namespace Siace.Data.Models;

public partial class Contestacione
{
    public int ConId { get; set; }

    public int ConUsrId { get; set; }

    public int ConEncId { get; set; }

    public DateTime ConFecha { get; set; }

    public string ConNombre { get; set; } = null!;

    public DateTime? ConFechaFin { get; set; }

    public virtual Encuesta ConEnc { get; set; } = null!;

    public virtual ICollection<ContestacionesRespuesta> ContestacionesRespuesta { get; set; } = new List<ContestacionesRespuesta>();
}
