using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Link
{
    public long ID { get; set; }

    public string? LinkTitel { get; set; }

    public string? LinkPfad { get; set; }

    public virtual ICollection<Modell> Modell { get; } = new List<Modell>();
}
