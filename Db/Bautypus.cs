using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Bautypus
{
    public long ID { get; set; }

    public string? Bautypus1 { get; set; }

    public virtual ICollection<Modell> Modell { get; } = new List<Modell>();
}
