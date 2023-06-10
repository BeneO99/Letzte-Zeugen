using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Ort
{
    public long ID { get; set; }

    public string? Koordinate { get; set; }

    public virtual ICollection<Institute> Institute { get; } = new List<Institute>();
}
