using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Person
{
    public long ID { get; set; }

    public string? NachnameVorname { get; set; }

    public virtual ICollection<Modell> ModellIDEigentuemerNavigation { get; } = new List<Modell>();

    public virtual ICollection<Modell> ModellIDUrheberNavigation { get; } = new List<Modell>();
}
