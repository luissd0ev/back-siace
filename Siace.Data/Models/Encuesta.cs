using System;
using System.Collections.Generic;

namespace Siace.Data.Models;

public partial class Encuesta
{
    public int EncId { get; set; }

    public string EncDescripcion { get; set; } = null!;

    public bool EncActivo { get; set; }

    public DateTime EncFechaRegistro { get; set; }

    public int EncUsrIdRegistro { get; set; }

    public virtual ICollection<Contestacione> Contestaciones { get; set; } = new List<Contestacione>();

    public virtual ICollection<Pregunta> Pregunta { get; set; } = new List<Pregunta>();
}
