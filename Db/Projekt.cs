using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Projekt
{
    public long ID { get; set; }

    public string? Projekt1 { get; set; }

    public long? Standort { get; set; }

    public long? IDRealisiert { get; set; }

    public virtual Realisiert? IDRealisiertNavigation { get; set; }

    public virtual ICollection<Modell> Modell { get; } = new List<Modell>();
}
