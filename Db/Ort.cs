using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Ort
{
    public long ID { get; set; }

    public string? Koordinate { get; set; }

    public virtual ICollection<Institute> Institute { get; } = new List<Institute>();

    public virtual ICollection<Modell> ModellErstellungsortNavigation { get; } = new List<Modell>();

    public virtual ICollection<Modell> ModellStandortNavigation { get; } = new List<Modell>();
}
