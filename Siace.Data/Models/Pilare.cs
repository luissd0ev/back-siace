using System;
using System.Collections.Generic;

namespace Siace.Data.Models;

public partial class Pilare
{
    public int PilId { get; set; }

    public string PilDesc { get; set; } = null!;

    public double PilPond { get; set; }

    public virtual ICollection<Pregunta> Pregunta { get; set; } = new List<Pregunta>();
}
