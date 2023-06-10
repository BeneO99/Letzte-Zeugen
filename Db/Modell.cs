using System;
using System.Collections.Generic;

namespace Letzte_Zeugen.Db;

public partial class Modell
{
    public long ID { get; set; }

    public string? Ausschnitt { get; set; }

    public string? Bezeichnung { get; set; }

    public string? ErstellungszeitraumModell { get; set; }

    public string? ErstellungszeiraumBauwerk { get; set; }

    public string? AbmessungenLxBxH { get; set; }

    public string? Kapitel { get; set; }

    public string? Seitenzahl { get; set; }

    public string? Spannweite { get; set; }

    public long? Standort { get; set; }

    public long? Erstellungsort { get; set; }

    public string? Massstab { get; set; }

    public string? BauwerkLink { get; set; }

    public long? IDExistent { get; set; }

    public long? IDZustand { get; set; }

    public long? IDGefaehrdung { get; set; }

    public long? IDMessmodell { get; set; }

    public long? IDBautypus { get; set; }

    public long? IDMaterial { get; set; }

    public long? IDProjekt { get; set; }

    public long? IDEigentuemer { get; set; }

    public long? IDUrheber { get; set; }

    public long? IDPruefinstitut { get; set; }

    public long? IDBeteiligteInstitute { get; set; }

    public long? IDLink { get; set; }
}
