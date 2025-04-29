using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp1_MeinHundNanga;

public class ClsMenuEntryKividOrGEOgraf : IEquatable<ClsMenuEntryKividOrGEOgraf>
{
    public ClsMenuEntryKividOrGEOgraf()
    {
        FullPath = "SI.CA.leerString";
        SearchPath = "SI.CA.leerString";
        DerOrt = Auftragsart.Undefined;
        //Main = false;
    }
    public string Name => StaticMethods.GetName(FullPath, SearchPath);

    public bool Main => MethodsAndClassesOrders.FnkIsMainFile(FullPath, SearchPath);

    //public bool Main { get; set; }
    public string FullPath { get; set; }
    //public byte SearchPathDepth { get; set; }     // SearchPathDepth gehört hier hin und nicht in die Klasse ClsMenuEntryBase
    public string SearchPath { get; set; }

    public Auftragsart? DerOrt { get; set; }

    public Guid TheGuid { get; set; }

    public bool Equals(ClsMenuEntryKividOrGEOgraf? other)
    {
        if (other == null)
        {
            return false;
        }

        if (FullPath.Equals(other.FullPath, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        if (FullPath == null)
        {
            return 0.GetHashCode();
        }
        else
        {
            return FullPath.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ClsMenuEntryKividOrGEOgraf);
    }
}

public class ClsMenuEntryFolder : IEquatable<ClsMenuEntryFolder>
{
    public ClsMenuEntryFolder()
    {
        FullPath = "SI.CA.leerString";
        SearchPath = "SI.CA.leerString";
    }
    public string FullPath { get; set; }
    //public byte SearchPathDepth { get; set; }
    public string SearchPath { get; set; }
    public Guid TheGuid { get; set; }
    public Auftragsart Ort { get; set; }

    public bool Equals(ClsMenuEntryFolder? other)
    {
        if (other == null)
        {
            return false;
        }

        if (FullPath.Equals(other.FullPath, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode() => FullPath.GetHashCode(StringComparison.OrdinalIgnoreCase);

    public override bool Equals(object? obj)
    {
        return Equals(obj as ClsMenuEntryFolder);
    }
}

public class ClsStatusDerUebernahme
{
    public ClsStatusDerUebernahme()
    {
        EnumStatus = StatusDerUebernahme.Unbekannt;
        StrCaption = "(unbekannt)";
    }
    public StatusDerUebernahme EnumStatus { get; set; }
    public string StrCaption { get; set; }
}

/// <summary>
/// Diese Klasse wird für die Hilfsliste gebraucht. Zuerst wird die Hilfsliste gefüllt, dann wird aus der Hilfsliste mit Hilfe von "GroupBy" die Liste 
/// "ListFoundEntriesCQ" im VM Auftragsstarter gefüllt.   (Neuer Name: _ocListOfFolders)
/// </summary>
public class ClsMenuEntry : IEquatable<ClsMenuEntry>
{
    public ClsMenuEntry()
    {
        EntryFullPath = "SI.CA.leerString";
        EntryBasePath = "SI.CA.leerString";
    }
    public TypeOfFile? EntryType { get; set; }  // 0 = Kivid-Datei | 1 = Geograf-Datei | 2 = Ordner
    public string EntryFullPath { get; set; }   // z.B. "F:\AUFTRÄGE\18-0446\18-0446.KIT" oder "F:\AUFTRÄGE\19-0356"
    public Auftragsart? EntryOrt { get; set; }  // 0 = AUFTRÄGE | 1 = Angebote | 2 = Archiv | 3 = VB-Marburg | 4 = Brauroth-Haxter | 5 = Abzeichnung der Flurkarte | 6 = Angebot Archiv
    public EntryFoundSource EntryFoundSource { get; set; }   // 0 = normale Suche; 1 = Suche über GEObüro mittels Brauroth&Haxter-Auftragsnummer; 2 = Über die Suchen nach der FFV-Nummer gefunden
    public string EntryBasePath { get; set; }
    public string StrName => StaticMethods.GetName(EntryFullPath, EntryBasePath);
    public string StrSortName => MthStrSortName();

    private string MthStrSortName()
    {
        string _ssn;
        int intDepthForSearchPath, entrySearchPathDepth;
        string[] strList = EntryFullPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.None);
        StringBuilder stringBuilderTemp;
        entrySearchPathDepth = EntryBasePath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.None).Length;

        if (entrySearchPathDepth < strList.Length)
        {
            stringBuilderTemp = new();
            intDepthForSearchPath = entrySearchPathDepth + 1;  // not zero based
            for (int i = entrySearchPathDepth; i < intDepthForSearchPath; i++)
            {
                if (strList.GetUpperBound(0) >= i)
                {
                    stringBuilderTemp.Append(string.Concat(strList[i], Path.DirectorySeparatorChar));
                }
            }
            _ssn = stringBuilderTemp.ToString().TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, ' ');
        }
        else
        {
            _ssn = EntryFullPath;
        }

        return string.Concat(EntryOrt.ToString(), "__", _ssn);
    }


    public bool Equals(ClsMenuEntry? other)
    {
        if (other == null)
        {
            return false;
        }

        if (EntryFullPath.Equals(other.EntryFullPath, StringComparison.OrdinalIgnoreCase) && EntryOrt.Equals(other.EntryOrt) && EntryType.Equals(other.EntryType))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(EntryFullPath, StringComparer.OrdinalIgnoreCase);
        hash.Add(EntryOrt);
        hash.Add(EntryType);
        return hash.ToHashCode();
    }// => (EntryType, EntryFullPath, EntryOrt).GetHashCode();

    public override bool Equals(object? obj) => Equals(obj as ClsMenuEntry);
}