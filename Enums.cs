using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1_MeinHundNanga;

public enum EntryFoundSource
{
    GefundenUeberNormaleSuche,
    GefundenUeberBRHXTRNummer,
    GefundenUeberFFVnummer
}

public enum Auftragsart
{
    Undefined = -1,
    Auftraege = 0,
    Angebote = 1,
    Archiv = 2,
    VB_Frankfurt = 3,
    AbzeichnungDerFlurkarte = 4,
    VB_Frankfurt_Angebot = 5,
    AngeboteArchiv = 6
}

public enum StatusDerUebernahme
{
    Unbekannt,
    NichtEingereicht,
    Eingereicht,
    Uebernommen
}

public enum TypeOfFile
{
    KIVIDfile = 0,
    GEOgrafFile = 1,
    Folder = 2
}