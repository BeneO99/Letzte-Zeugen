﻿using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Messmodell
{
    public long ID { get; set; }

    public string? Messmodell1 { get; set; }

    public virtual ICollection<Modell> Modell { get; } = new List<Modell>();
}