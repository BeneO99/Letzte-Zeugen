using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Realisiert
{
    public long ID { get; set; }

    public string? Realisiert1 { get; set; }

    public virtual ICollection<Projekt> Projekt { get; } = new List<Projekt>();
}
