﻿using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Institute
{
    public long ID { get; set; }

    public string? Name { get; set; }

    public long? Koordinate { get; set; }

    public virtual Ort? KoordinateNavigation { get; set; }
}
