using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Abbildung
{
    public long ID { get; set; }

    public string? Titel { get; set; }

    public string? Pfad { get; set; }

    public virtual ICollection<Modell> Modell { get; } = new List<Modell>();
}
