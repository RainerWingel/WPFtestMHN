using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1_MeinHundNanga;

public class MethodsAndClassesOrders
{
    /// <summary>
    /// Legt fest, ob es sich um die Kivid/GEOgraf-Datei um die Hauptdatei handelt.
    /// </summary>
    /// <param name="strFullPath">Voller Pfad zur Kivid- oder GEOgraf-Datei.</param>
    /// <param name="BasePath">Pfad zum Haupt-Auftragsordner.</param>
    /// <returns>Gibt true zurück, wenn es die Hauptdatei ist und false wenn es nicht die Hauptdatei ist.</returns>
    public static bool FnkIsMainFile(string strFullPath, string BasePath)
    {
        string strPathOnly = Path.GetDirectoryName(strFullPath) ?? "SI.CA.leerString";
        string strSuspectedFolder;

        if (strPathOnly.Contains(Path.DirectorySeparatorChar))
        {
            strSuspectedFolder = string.Concat(strPathOnly[(strPathOnly.LastIndexOf(Path.DirectorySeparatorChar) + 1)..], Path.DirectorySeparatorChar, strPathOnly[(strPathOnly.LastIndexOf(Path.DirectorySeparatorChar) + 1)..]);
        }
        else if (strPathOnly.Contains(Path.AltDirectorySeparatorChar))
        {
            strSuspectedFolder = string.Concat(strPathOnly[(strPathOnly.LastIndexOf(Path.AltDirectorySeparatorChar) + 1)..], Path.AltDirectorySeparatorChar, strPathOnly[(strPathOnly.LastIndexOf(Path.AltDirectorySeparatorChar) + 1)..]);
        }
        else
        {
            strSuspectedFolder = strPathOnly;
        }

        string strFileNameWithoutExt = Path.GetFileNameWithoutExtension(strFullPath);

        string strSuspectedPath = Path.Combine(BasePath, strSuspectedFolder);
        string strRealPath = Path.Combine(strPathOnly, strFileNameWithoutExt);
        if (strSuspectedPath.Equals(strRealPath, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
