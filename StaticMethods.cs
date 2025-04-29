using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1_MeinHundNanga;

public class StaticMethods
{
    public static string GetName(string FullPath, string BasePath)
    {
        string strName;
        StringBuilder strBuilder = new();
        string[] strArray = FullPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.None);
        int intCountFullPath = strArray.Length;
        int intCountSearchPath = BasePath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.None).Length;
        if (intCountSearchPath < intCountFullPath)
        {
            strBuilder.Append(Path.DirectorySeparatorChar);
            for (int i = intCountSearchPath; i < intCountFullPath; i++)
            {
                strBuilder.Append(strArray[i] + Path.DirectorySeparatorChar);
            }
            strName = strBuilder.ToString().TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, ' ');
        }
        else
        {
            strName = FullPath;
        }

        if (strName.Length > 350)
        {
            strName = string.Concat("... ", strName.AsSpan(strName.Length - 350, 350));
        }

        return strName;
    }
}
