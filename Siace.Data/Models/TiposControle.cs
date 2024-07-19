using System;
using System.Collections.Generic;

namespace Siace.Data.Models;

public partial class TiposControle
{
    public int TicId { get; set; }

    public string TicDescripcion { get; set; } = null!;

    public virtual ICollection<TiposPregunta> TiposPregunta { get; set; } = new List<TiposPregunta>();
}
