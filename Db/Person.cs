﻿using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Person
{
    public long ID { get; set; }

    public string? Nachname { get; set; }

    public string? Vorname { get; set; }
}
